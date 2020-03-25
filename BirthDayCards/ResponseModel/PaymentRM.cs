using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BirthDayCards.ResponseModel
{
    public class PaymentRM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Cvc { get; set; }
        public int Amount { get; set; }
    }
}
