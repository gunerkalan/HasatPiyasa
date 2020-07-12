using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class Cities
    {
        public Cities()
        {
            Tuiks = new HashSet<Tuiks>();
            DataInputs = new HashSet<DataInputs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SubeId { get; set; }

        public virtual Subes Sube { get; set; }
        public virtual ICollection<Tuiks> Tuiks { get; set; }
        public virtual ICollection<DataInputs> DataInputs { get; set; }
    }
}
