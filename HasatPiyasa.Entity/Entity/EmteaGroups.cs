using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class EmteaGroups : BaseEntity
    {
        public EmteaGroups()
        {
            EmteaTypes = new HashSet<EmteaTypes>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public int EmteaId { get; set; }

        public virtual Emteas Emtea { get; set; }
        public virtual ICollection<EmteaTypes> EmteaTypes { get; set; }
    }
}
