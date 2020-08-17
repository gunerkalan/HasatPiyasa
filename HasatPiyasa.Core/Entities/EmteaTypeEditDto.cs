using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class EmteaTypeEditDto
    {
        public int Id { get; set; }
        public int EmteaId { get; set; }
        public int EmteaGroupId { get; set; }
        public string EmteaTypeName { get; set; }
    }
}
