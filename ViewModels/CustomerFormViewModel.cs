using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibApp.ViewModels
{
    public class CustomerFormViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please provide customer's name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool HasNewsletterSubscribed { get; set; }

        [Display(Name = "Membership Type")]
        [Required(ErrorMessage = "Please provide Membership Type")]
        public byte? MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfMember]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "Password must contain at least 4 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        public string RoleId { get; set; }

        public IEnumerable<MembershipTypeDto> MembershipTypes { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

        public string Title
        {
            get
            {
                return !string.IsNullOrEmpty(Id) ? "Edit Customer" : "New Customer";
            }
        }

        public CustomerFormViewModel()
        {
            Id = string.Empty;
        }

        public CustomerFormViewModel(CustomerDto customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            MembershipTypeId = customer.MembershipTypeId;
            HasNewsletterSubscribed = customer.HasNewsletterSubscribed;
            Birthdate = customer.Birthdate;
            Email = customer.Email;
            RoleId = customer.RoleId;
        }
    }
}