using HasatPiyasa.Entity.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Entity.Entity
{
    public class UserRoles : BaseEntity
    {
        public UserRoles()
        {
            Users = new HashSet<Users>();
        }
        public int Id { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
