using AutoMapper;
using Flurl.Http;
using LibApp.Dtos;
using LibApp.Models;
using LibApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Customer> signInManager;
        private readonly UserManager<Customer> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        private const string membershipTypesUrl = "https://localhost:44352/api/membershipTypes";

        public AccountController(SignInManager<Customer> signInManager, UserManager<Customer> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.FindByEmailAsync(loginModel.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Incorrect email or password";

            return View(loginModel);
        }


        public async Task<IActionResult> Register()
        {
            var membershipTypes = await membershipTypesUrl.GetJsonAsync<IEnumerable<MembershipTypeDto>>();
            var viewModel = new RegisterViewModel
            {
                Roles = roleManager.Roles.ToList(),
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<Customer>(registerViewModel);
                user.UserName = registerViewModel.Email;
                var result = await userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    var roles = roleManager.Roles.ToList();
                    var role = roles.FirstOrDefault(r => r.Id == registerViewModel.RoleId);
                    await userManager.AddToRoleAsync(user, role.Name);

                    return RedirectToAction("Login", "Account");
                }
            }

            var membershipTypes = await membershipTypesUrl.GetJsonAsync<IEnumerable<MembershipTypeDto>>();
            registerViewModel.MembershipTypes = membershipTypes;
            registerViewModel.Roles = roleManager.Roles.ToList();

            return View(registerViewModel);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
