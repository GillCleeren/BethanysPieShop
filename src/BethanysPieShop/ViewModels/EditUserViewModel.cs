using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BethanysPieShop.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter the user name")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter the user email")]
        public string Email { get; set; }

        public List<string> UserClaims { get; set; }

        [Required(ErrorMessage = "Please enter the birth date")]
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}