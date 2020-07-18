using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class EmteaGrupDto
    {
        public int Id { get; set; }
        public DateTime AddedTime { get; set; }
        public string EmteaGrupAd { get; set; }
        public string EmteaName { get; set; }
        public string EmteaKod { get; set; }
    }
}
