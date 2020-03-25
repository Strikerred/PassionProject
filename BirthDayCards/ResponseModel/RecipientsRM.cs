using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthDayCards.ResponseModel
{
    public class RecipientsRM
    {
        public int BdayId { get; set; }
        public IEnumerable<string> recipients { get; set; }
    }
}
