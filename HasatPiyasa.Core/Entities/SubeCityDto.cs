using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class SubeCityDto
    {
        public int SubeId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string SubeName { get; set; }

    }
}
