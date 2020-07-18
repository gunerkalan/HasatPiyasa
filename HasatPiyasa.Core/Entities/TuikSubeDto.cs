using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class TuikSubeDto
    {
        public int Id { get; set; }
        public DateTime AddedTime { get; set; }
        public int TuikYear { get; set; }
        public string SubeName { get; set; }
        public string EmteaTypeName { get; set; }
        public string AddedUser { get; set; }
        public string EmteaGroupName { get; set; }
        public string EmteaName { get; set; }
        public double TuikValue { get; set; }
        public string EmteaCode { get; set; }
    }
}
