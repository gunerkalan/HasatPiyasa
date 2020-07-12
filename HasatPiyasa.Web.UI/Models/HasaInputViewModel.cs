using HasatPiyasa.Entity.Entity;
using System;
using System.Linq;

namespace HasatPiyasa_Web_UI.Models
{
    public class HasaInputViewModel
    {
        public Emteas Emteas { get; set; }
        public EmteaGroups EmteaGroups { get; set; };
        public EmteaTypeGroups EmteaTypeGroups { get; set; }
        public EmteaTypes EmteaTypes { get; set; }

    }
}
