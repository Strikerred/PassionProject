using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthDayCards.Models
{
    public partial class Users
    {
        public Users()
        {
            BdayPerson = new HashSet<BdayPerson>();
            Payments = new HashSet<Payments>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserName { get; set; }
        public int? RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<BdayPerson> BdayPerson { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
    }
}
