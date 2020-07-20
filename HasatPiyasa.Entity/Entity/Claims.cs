using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class Claims : BaseEntity
    {
        public Claims()
        {
            UserClaims = new HashSet<UserClaims>();
        }

        public int Id { get; set; }
        public string ClaimName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserClaims> UserClaims { get; set; }
    }
}
