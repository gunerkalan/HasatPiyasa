using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class DataInputTbaleListModelForRiceDto
    {
        public int Id { get; set; }
        public string SubeName { get; set; }
        public string Cityname { get; set; }
        public string EmteaName { get; set; }
        public string EmteaGroupName { get; set; }
        public string EmteaTypeName { get; set; }
        public string AddUser { get; set; }
        public string UpdateUser { get; set; }
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
        public int FormDataInputId { get; set; }
    }
}
