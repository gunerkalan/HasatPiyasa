using HasatPiyasa.Entity.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.Models
{
    public class EmteaGoupAddModel
    {
        public List<SelectListItem> Emteas { get; set; }
        public EmteaGroups EmteaGroup { get; set; }
    }
}
