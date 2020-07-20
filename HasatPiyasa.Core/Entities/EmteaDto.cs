using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class EmteaDto
    {
        public int Id { get; set; }
        public string EmteaCode { get; set; }
        public string EmteaName { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
