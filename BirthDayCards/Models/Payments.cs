using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthDayCards.Models
{
    public partial class Payments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }

        public virtual Users UserNameNavigation { get; set; }
    }
}
