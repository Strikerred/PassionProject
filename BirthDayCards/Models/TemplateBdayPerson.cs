using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthDayCards.Models
{
    public partial class TemplateBdayPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TempBday { get; set; }
        public int? BdayId { get; set; }
        public int? TemplateId { get; set; }

        public virtual BdayPerson Bday { get; set; }
        public virtual Template Template { get; set; }
    }
}
