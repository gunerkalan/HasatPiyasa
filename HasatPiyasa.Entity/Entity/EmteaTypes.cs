using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class EmteaTypes : BaseEntity
    {
        public EmteaTypes()
        {
            DataInputs = new HashSet<DataInputs>();
            EmteaTypeGroups = new HashSet<EmteaTypeGroups>();
            Tuiks = new HashSet<Tuiks>();
        }

        public int Id { get; set; }
        public string EmteaTypeName { get; set; }
        public int EmteaGroupId { get; set; }

        public virtual EmteaGroups EmteaGroup { get; set; }
        public virtual ICollection<DataInputs> DataInputs { get; set; }
        public virtual ICollection<EmteaTypeGroups> EmteaTypeGroups { get; set; }
        public virtual ICollection<Tuiks> Tuiks { get; set; }
    }
}
