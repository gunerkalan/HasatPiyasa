using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class Bolges : BaseEntity
    {
        public Bolges()
        {
            Subes = new HashSet<Subes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
       
        public virtual ICollection<Subes> Subes { get; set; }
    }
}
