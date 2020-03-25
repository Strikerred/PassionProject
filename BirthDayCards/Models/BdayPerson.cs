using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthDayCards.Models
{
    public partial class BdayPerson
    {
        public BdayPerson()
        {
            BdEvent = new HashSet<BdEvent>();
            Recipients = new HashSet<Recipients>();
            TemplateBdayPerson = new HashSet<TemplateBdayPerson>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BdayId { get; set; }
        public string BdFirstName { get; set; }
        public string BdLastName { get; set; }
        public string BdComingAge { get; set; }

        public virtual ICollection<BdEvent> BdEvent { get; set; }
        public virtual ICollection<Recipients> Recipients { get; set; }
        public virtual ICollection<TemplateBdayPerson> TemplateBdayPerson { get; set; }
    }
}
