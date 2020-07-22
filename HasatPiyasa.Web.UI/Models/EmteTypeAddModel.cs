using HasatPiyasa.Entity.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.Models
{
    public class EmteTypeAddModel
    {
        public EmteaTypes EmteaTypes { get; set; }
        public List<SelectListItem> EmteaGroups { get; set; }
        public List<SelectListItem> Emteas { get; set; }
    }

}
