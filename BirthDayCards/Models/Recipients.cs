using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthDayCards.Models
{
    public partial class Recipients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipientsId { get; set; }
        public string Recipients1 { get; set; }
        public int? BdayId { get; set; }

        public virtual BdayPerson Bday { get; set; }
    }
}
