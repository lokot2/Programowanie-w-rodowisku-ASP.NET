using LibApp.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Dtos
{
    public class CustomerDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please provide customer's name")]
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

        public string Password { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        public string RoleId { get; set; }
    }
}