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
            Payments = new HashSet<Payments>();
            PersonAccount = new HashSet<PersonAccount>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserName { get; set; }
        public int? RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
        public virtual ICollection<PersonAccount> PersonAccount { get; set; }
    }
}
