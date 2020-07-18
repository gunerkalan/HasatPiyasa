using HasatPiyasa.Entity.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Entity.Entity
{
    public class FormDataInput : BaseEntity
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
