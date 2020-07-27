using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class FormDataInput : BaseEntity
    {
        public FormDataInput()
        {
            DataInputs = new HashSet<DataInputs>();
        }

        public int Id { get; set; }
        public bool IsLock { get; set; }
        public int CityId { get; set; }
        public int SubeId { get; set; }
        public int EmteaId { get; set; }

        public virtual Cities City { get; set; }
        public virtual Subes Sube { get; set; }

        public virtual ICollection<DataInputs> DataInputs { get; set; }
    }
}
