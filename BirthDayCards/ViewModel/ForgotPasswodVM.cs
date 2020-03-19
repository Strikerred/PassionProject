using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BirthDayCards.ViewModel
{
    public class ForgotPasswodVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
