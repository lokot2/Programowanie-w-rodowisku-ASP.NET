using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibApp.ViewModels;
using Flurl;
using LibApp.Dtos;
using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using LibApp.Models;
using AutoMapper;

namespace LibApp.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Customer> userManager;
        private readonly IMapper mapper;

        private const string customersUrl = "https://localhost:44352/api/customers";
        private const string membershipTypesUrl = "https://localhost:44352/api/membershipTypes";

        public CustomersController(RoleManager<IdentityRole> roleManager, UserManager<Customer> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public ViewResult Index()
        {          
            return View();
        }

        [Authorize(Roles = "StoreManager,Owner")]
        public async Task<IActionResult> Details(string id)
        {
            var customer = await customersUrl.AppendPathSegment(id)
                .GetJsonAsync<CustomerDto>();

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> New()
        {
            var membershipTypes = await membershipTypesUrl.GetJsonAsync<IEnumerable<MembershipTypeDto>>();
            var viewModel = new CustomerFormViewModel()
            {
                Roles = roleManager.Roles.ToList(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(string id)
        {
            var customer = await customersUrl.AppendPathSegment(id)
                .GetJsonAsync<CustomerDto>();

            if (customer == null)
            {
                return NotFound();
            }

            var membershipTypes = await membershipTypesUrl.GetJsonAsync<IEnumerable<MembershipTypeDto>>();
            var viewModel = new CustomerFormViewModel(customer)
            {
                Roles = roleManager.Roles.ToList(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Save(CustomerDto customer)
        {
            if (!ModelState.IsValid)
            {
                var membershipTypes = await membershipTypesUrl.GetJsonAsync<IEnumerable<MembershipTypeDto>>();
                var viewModel = new CustomerFormViewModel(customer)
                {
                    Roles = roleManager.Roles.ToList(),
                    MembershipTypes = membershipTypes
                };

                return View("CustomerForm", viewModel);
            }

            if (string.IsNullOrEmpty(customer.Id))
            {
                var user = mapper.Map<Customer>(customer);
                user.UserName = customer.Email;
                var result = await userManager.CreateAsync(user, customer.Password);

                if (result.Succeeded)
                {
                    var roles = roleManager.Roles.ToList();
                    var role = roles.FirstOrDefault(r => r.Id == customer.RoleId);
                    await userManager.AddToRoleAsync(user, role.Name);

                    return RedirectToAction("Index", "Customers");
                }
            }
            else
            {
                await customersUrl.PutJsonAsync(customer);
            }

            return RedirectToAction("Index", "Customers");
        }
    }
}