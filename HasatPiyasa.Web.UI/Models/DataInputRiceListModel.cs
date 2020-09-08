using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.Models
{
    public class DataInputRiceListModel
    {
        public FormDataInput FormDataInput { get; set; }
    }

    public  class DataInputsDTO 
    {
        public int Id { get; set; }
        public int SubeId { get; set; }
        public int CityId { get; set; }
        public int EmteaId { get; set; }
        public int EmteaGroupId { get; set; }
        public int EmteaTypeId { get; set; }
        public int AddUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public int AlimYear { get; set; }
        public decimal TuikValue { get; set; }
        public decimal GuessValue { get; set; }
        public decimal HasatOran { get; set; }
        public decimal HasatMiktar { get; set; }
        public decimal UreticiKalanMiktar { get; set; }
        public string Natural1 { get; set; }
        public string Natural2 { get; set; }
        public string Natural3 { get; set; }
        public string Natural4 { get; set; }
        public string Natural5 { get; set; }
        public string NaturalToplam { get; set; }
        public string ToptanPiyasa1 { get; set; }
        public string ToptanPiyasa2 { get; set; }
        public string ToptanPiyasa3 { get; set; }
        public string ToptanPiyasa4 { get; set; }
        public string ToptanPiyasa5 { get; set; }
        public string ToptanPiyasaToplam { get; set; }
        public string Perakende1 { get; set; }
        public string Perakende2 { get; set; }
        public string Perakende3 { get; set; }
        public string Perakende4 { get; set; }
        public string Perakende5 { get; set; }
        public string Perakende6 { get; set; }
        public string PerakendeToplam { get; set; }
       


    }
}
