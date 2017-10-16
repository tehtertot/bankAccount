using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bank.Models
{
    public class RegisterUserModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage="No special characters.")]
        [Display(Name="First Name")]
        public string FirstName {get;set;}
        
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage="No special characters.")]
        [Display(Name="Last Name")]
        public string LastName {get;set;}

        [Required]
        [EmailAddress]
        [Display(Name="Email Address")]
        public string EmailAddress {get;set;}

        [Required]
        [MinLength(8)]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword {get;set;}
    }
}