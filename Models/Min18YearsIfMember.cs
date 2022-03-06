using LibApp.Dtos;
using LibApp.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class Min18YearsIfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is RegisterViewModel)
            {
                var customer = (RegisterViewModel)validationContext.ObjectInstance;
                if (customer.MembershipTypeId == MembershipType.Unknown ||
                    customer.MembershipTypeId == MembershipType.PayAsYouGo)
                {
                    return ValidationResult.Success;
                }

                if (customer.Birthdate == null)
                {
                    return new ValidationResult("Birthdate is required");
                }

                var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

                return age >= 18
                    ? ValidationResult.Success
                    : new ValidationResult("Customer should be at least 18 year old to subscribe");
            }
            else
            {
                var customer = (CustomerDto)validationContext.ObjectInstance;
                if (customer.MembershipTypeId == MembershipType.Unknown ||
                    customer.MembershipTypeId == MembershipType.PayAsYouGo)
                {
                    return ValidationResult.Success;
                }

                if (customer.Birthdate == null)
                {
                    return new ValidationResult("Birthdate is required");
                }

                var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

                return age >= 18
                    ? ValidationResult.Success
                    : new ValidationResult("Customer should be at least 18 year old to subscribe");
            }
        }
    }
}