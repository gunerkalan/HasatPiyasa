using HasatPiyasa.Entity.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.Models
{
    public class IsLockModel
    {
        public List<Emteas> Emteas { get; set; }
        
    }

    public class SetFormDataState
    {
        public string CityName { get; set; }
        public int FormId { get; set; }
        public int CityId { get; set; }
        public string FormDataDate { get; set; }
        public bool State { get; set; }
        public int SubeId { get; set; }
        public string SubeName { get; set; }
    }
}
