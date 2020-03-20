using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthDayCards.ResponseModel
{
    public class SuccessPaymentRM
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
    }
}
