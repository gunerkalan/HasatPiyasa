using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class Subes : BaseEntity
    {
        public Subes()
        {
            Cities = new HashSet<Cities>();
            DataInputs = new HashSet<DataInputs>();
            Tuiks = new HashSet<Tuiks>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public int BolgeId { get; set; }
        public string SubeKod { get; set; }
        public string SubeName { get; set; }

        public virtual Bolges Bolge { get; set; }
        public virtual ICollection<Cities> Cities { get; set; }
        public virtual ICollection<DataInputs> DataInputs { get; set; }
        public virtual ICollection<Tuiks> Tuiks { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
