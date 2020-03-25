using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthDayCards.Models
{
    public partial class BdEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        public int? BdayId { get; set; }
        public string Ename { get; set; }
        public string EphoneNumber { get; set; }
        public string Eemail { get; set; }
        public DateTime Edate { get; set; }
        public string Etime { get; set; }
        public string Eaddress { get; set; }
        public string Ecity { get; set; }
        public string Eprovince { get; set; }
        public string EpostalCode { get; set; }

        public virtual BdayPerson Bday { get; set; }
    }
}
