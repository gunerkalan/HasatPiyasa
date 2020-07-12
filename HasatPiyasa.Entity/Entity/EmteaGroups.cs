using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class EmteaGroups : BaseEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int? EmteaId { get; set; }

        public virtual Emteas Emtea { get; set; }
    }
}
