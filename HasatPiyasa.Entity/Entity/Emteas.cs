using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class Emteas : BaseEntity
    {
        public Emteas()
        {
            EmteaGroups = new HashSet<EmteaGroups>();
            EmteaTypes = new HashSet<EmteaTypes>();
        }

        public int Id { get; set; }
        public string EmteaName { get; set; }
        public string EmteaCode { get; set; }

        public virtual ICollection<EmteaGroups> EmteaGroups { get; set; }
        public virtual ICollection<EmteaTypes> EmteaTypes { get; set; }
    }
}
