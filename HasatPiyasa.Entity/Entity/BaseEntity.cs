using HasatPiyasa.Entity.DataAccess.Entities;
using System;

namespace HasatPiyasa.Entity.Entity
{
    public class BaseEntity:IEntity
    {
        public DateTime AddedTime { get; set; }
        public Nullable<DateTime> UpdatedTime { get; set; }
        public bool IsActive { get; set; }
    }
}
