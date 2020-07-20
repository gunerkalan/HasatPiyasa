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
       

        public virtual ICollection<DataInputs> DataInputs { get; set; }
    }
}
