using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class UserClaims : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClaimId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Claims Claim { get; set; }
        public virtual Users User { get; set; }
    }
}
