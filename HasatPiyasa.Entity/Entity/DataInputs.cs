using System;
using System.Collections.Generic;

namespace HasatPiyasa.Entity.Entity
{
    public partial class DataInputs : BaseEntity
    {
        public int Id { get; set; }
        
        public int SubeId { get; set; }
        public int FormDataInputId { get; set; }
        public int CityId { get; set; }
        public int EmteaTypeId { get; set; }
        public int AddUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public int AlimYear { get; set; }
        public double TuikValue { get; set; }
        public double GuessValue { get; set; }
        public double HasatOran { get; set; }
        public double HasatMiktar { get; set; }
        public double UreticiKalanMiktar { get; set; }
        public double? Natural1 { get; set; }
        public double? Natural2 { get; set; }
        public double? Natural3 { get; set; }
        public double? Natural4 { get; set; }
        public double? Natural5 { get; set; }
        public double? NaturalToplam { get; set; }
        public double? ToptanPiyasa1 { get; set; }
        public double? ToptanPiyasa2 { get; set; }
        public double? ToptanPiyasa3 { get; set; }
        public double? ToptanPiyasa4 { get; set; }
        public double? ToptanPiyasa5 { get; set; }
        public double? ToptanPiyasaToplam { get; set; }
        public double? Perakende1 { get; set; }
        public double? Perakende2 { get; set; }
        public double? Perakende3 { get; set; }
        public double? Perakende4 { get; set; }
        public double? Perakende5 { get; set; }
        public double? Perakende6 { get; set; }
        public double? PerakendeToplam { get; set; }

        public virtual Users AddUser { get; set; }
        public virtual Cities City { get; set; }
        public virtual EmteaTypes EmteaType { get; set; }
        public virtual Subes Sube { get; set; }
        public virtual Users UpdateUser { get; set; }
        public virtual FormDataInput FormDataInput { get; set; }
    }
}
