using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Customer's name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool HasNewsletterSubscribed { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        [Required(ErrorMessage = "Please provide Membership Type")]
        public byte MembershipTypeId { get; set; }

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

        public IEnumerable<IdentityRole> Roles { get; set; }

        public IEnumerable<MembershipTypeDto> MembershipTypes { get; set; }
    }
}