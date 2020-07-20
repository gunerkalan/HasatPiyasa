using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class SubeCities : BaseEntity
    {
        public int Id { get; set; }
        public int SubeId { get; set; }
        public int CityId { get; set; }
        
        public virtual Cities City { get; set; }
        public virtual Subes Sube { get; set; }
    }
}
