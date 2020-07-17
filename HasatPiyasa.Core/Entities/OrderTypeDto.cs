using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class OrderTypeDto
    {
        public int Id { get; set; }
        public DateTime AddedTime { get; set; }
        public string EmteaTypeName { get; set; }
        public string EmteaName { get; set; }
    }
}
