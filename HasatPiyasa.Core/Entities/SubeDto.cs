using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class SubeDto : SubeDtoCity
    {
        public string SubeName { get; set; }
        public string SubeCode { get; set; }
        public int Id { get; set; }
        public string BolgeName { get; set; }
        public List<SubeDtoCity> Cities { get; set; }
    }

    public class SubeDtoCity
    {
        public string CityName { get; set; }
    }
}
