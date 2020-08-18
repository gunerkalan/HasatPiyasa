using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class EmteaGroupTypeDto
    {
        public int Id { get; set; }
        public int EmteaId { get; set; }
        public int EmteaGroupId { get; set; }
        public int EmteaTypeId { get; set; }
        public string EmteaTypeGroupName { get; set; }
    }
}
