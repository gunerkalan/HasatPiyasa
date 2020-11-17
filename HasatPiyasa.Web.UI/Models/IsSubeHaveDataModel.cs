using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.Models
{
    public class IsSubeHaveDataModel
    {
        public List<Emteas> Emteas { get; set; }
    }

    public class DataState
    {
        public string SubeName { get; set; }
        public string SubeCode { get; set; } 
        public int Id { get; set; } 
        public string BolgeName { get; set; } 
        public string Cities { get; set; }
        public string HaveDataCities { get; set; }
        public string AddedDate { get; set; }
        public bool State { get; set; }
        public int IsHaveDataCount { get; set; }
    }
}
