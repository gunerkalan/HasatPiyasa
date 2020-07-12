using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class EmteaTypeGroups : BaseEntity
    {
        public int Id { get; set; }
        public string EmteaTypeGroupName { get; set; }
        public int? EmteaTypeId { get; set; }

        public virtual EmteaTypes EmteaType { get; set; }
    }
}
