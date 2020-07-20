using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class Users : BaseEntity
    {
        public Users()
        {
            DataInputsAddUser = new HashSet<DataInputs>();
            DataInputsUpdateUser = new HashSet<DataInputs>();
            TuiksAddUser = new HashSet<Tuiks>();
            TuiksUpdateUser = new HashSet<Tuiks>();
            UserClaims = new HashSet<UserClaims>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SicilNumber { get; set; }
        public string DomainUserName { get; set; }
        public bool IsDomain { get; set; }
        public string Password { get; set; }
        public int SubeId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }

        public virtual Subes Sube { get; set; }
        public virtual ICollection<DataInputs> DataInputsAddUser { get; set; }
        public virtual ICollection<DataInputs> DataInputsUpdateUser { get; set; }
        public virtual ICollection<Tuiks> TuiksAddUser { get; set; }
        public virtual ICollection<Tuiks> TuiksUpdateUser { get; set; }
        public virtual ICollection<UserClaims> UserClaims { get; set; }
    }
}
