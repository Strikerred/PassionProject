using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthDayCards.Models
{
    public partial class Template
    {
        public Template()
        {
            TemplateBdayPerson = new HashSet<TemplateBdayPerson>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemplateId { get; set; }
        public string TemplateUrl { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<TemplateBdayPerson> TemplateBdayPerson { get; set; }
    }
}
