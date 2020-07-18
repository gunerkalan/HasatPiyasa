+using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class Tuiks : BaseEntity
    {
        public int Id { get; set; }
      
        public int TuikYear { get; set; }
        public int? CityId { get; set; }
        public int? SubeId { get; set; }
        public int EmteaTypeId { get; set; }
        public int AddUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsCity { get; set; }
        public int? EmteaGroupId { get; set; }
        public double TuikValue { get; set; }
        public int GuessYear { get; set; }
        public double GuessValue { get; set; }

        public virtual Users AddUser { get; set; }
        public virtual Cities City { get; set; }
        public virtual EmteaTypes EmteaType { get; set; }
        public virtual Subes Sube { get; set; }
        public virtual Users UpdateUser { get; set; }
    }
}
