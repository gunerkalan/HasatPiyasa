using HasatPiyasa.Entity.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.Models
{
    public class UserAddModel
    {
        public Users User { get; set; }
        public List<SelectListItem> Subes { get; set; }
        public List<SelectListItem> UserRoles { get; set; }
    }
}
