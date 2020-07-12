using System;

namespace HasatPiyasa.Entity.Entity
{
    public class BaseEntity
    {
        public DateTime AddedTime { get; set; }
        public Nullable<DateTime> UpdatedTime { get; set; }
        public int IsActive { get; set; }
    }
}
