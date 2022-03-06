using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.MembershipTypes.Any())
            {
                Console.WriteLine("Database already seeded");
                return;
            }

            var membershipTypes = new List<MembershipType>
            {
                new MembershipType
                {
                    Id = 1,
                    Name = "Pay as You Go",
                    SignUpFee = 0,
                    DurationInMonths = 0,
                    DiscountRate = 0
                },
                new MembershipType
                {
                    Id = 2,
                    Name = "Monthly",
                    SignUpFee = 30,
                    DurationInMonths = 1,
                    DiscountRate = 10
                },
                new MembershipType
                {
                    Id = 3,
                    Name = "Quaterly",
                    SignUpFee = 90,
                    DurationInMonths = 3,
                    DiscountRate = 15
                },
                new MembershipType
                {
                    Id = 4,
                    Name = "Yearly",
                    SignUpFee = 300,
                    DurationInMonths = 12,
                    DiscountRate = 20
                }
            };

            context.MembershipTypes.AddRange(membershipTypes);

            context.Books.AddRange(
                new Book
                {
                    DateAdded = DateTime.Now.AddDays(new Random().Next(1, 30) * (-1)),
                    AuthorName = "Whitney G.",
                    Name = "Twój Carter",
                    GenreId = 3,
                    ReleaseDate = DateTime.Now.AddMonths(-3),
                    NumberAvailable = 2,
                    NumberInStock = 2
                },
                new Book
                {
                    DateAdded = DateTime.Now.AddDays(new Random().Next(1, 30) * (-1)),
                    AuthorName = "Evzen Bocek",
                    Name = "Ostatnia arystokratka",
                    GenreId = 4,
                    ReleaseDate = DateTime.Now.AddMonths(-8),
                    NumberAvailable = 1,
                    NumberInStock = 1
                },
                new Book
                {
                    DateAdded = DateTime.Now.AddDays(new Random().Next(1, 30) * (-1)),
                    AuthorName = "J.R.R. Tolkien",
                    Name = "Hobbit czyli tam i z powrotem",
                    GenreId = 6,
                    ReleaseDate = DateTime.Now.AddMonths(-21),
                    NumberAvailable = 5,
                    NumberInStock = 5
                });

            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            var user = new Customer
            {
                Email = "User@email.com",
                UserName = "User",
                Name = "Michał Jabłoński",
                Birthdate = DateTime.Now.AddYears(-44).AddMonths(-1),
                HasNewsletterSubscribed = true,
                MembershipTypeId = membershipTypes.First().Id
            };
            var storeManager = new Customer
            {
                Email = "StoreManager@email.com",
                UserName = "StoreManager",
                Name = "Alicja Juszczyk",
                Birthdate = DateTime.Now.AddYears(-33).AddMonths(-9),
                HasNewsletterSubscribed = false,
                MembershipTypeId = membershipTypes.Last().Id
            };
            var owner = new Customer
            {
                Email = "Owner@email.com",
                UserName = "Owner",
                Name = "Adam Sikora",
                Birthdate = DateTime.Now.AddYears(-49).AddMonths(-4),
                HasNewsletterSubscribed = false,
                MembershipTypeId = membershipTypes.First().Id
            };

            context.SaveChanges();

            var createUser = await userManager.CreateAsync(user, "User");
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }

            createUser = await userManager.CreateAsync(storeManager, "StoreManager");
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(storeManager, "StoreManager");
            }

            createUser = await userManager.CreateAsync(owner, "Owner");
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(owner, "Owner");
            }
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "User", "StoreManager", "Owner" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}