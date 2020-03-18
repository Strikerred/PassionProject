using System;
using System.Collections.Generic;

namespace BirthDayCards.Models
{
    public partial class Users
    {
        public string UserName { get; set; }
        public int? RoleId { get; set; }

        public virtual Roles Role { get; set; }
    }
}
