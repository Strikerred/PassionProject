using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BirthDayCards.ViewModel
{
    public class ResetPasswordRM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ResetToken { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
