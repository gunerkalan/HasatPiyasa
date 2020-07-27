using HasatPiyasa.Core.Entities;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.Models
{
    public class GreportRiceBySubeModel
    {
        public List<FormDataReportDto> FormDataInputs { get; set; }
        public List<Subes> Subes { get; set; }
    }
}
