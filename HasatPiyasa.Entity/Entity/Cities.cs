using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class Cities : BaseEntity
    {
        public Cities()
        {
            DataInputs = new HashSet<DataInputs>();
            Tuiks = new HashSet<Tuiks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SubeId { get; set; }
        public int Plaka { get; set; }
       
        public virtual Subes Sube { get; set; }
        public virtual ICollection<DataInputs> DataInputs { get; set; }
        public virtual ICollection<Tuiks> Tuiks { get; set; }
    }
}
