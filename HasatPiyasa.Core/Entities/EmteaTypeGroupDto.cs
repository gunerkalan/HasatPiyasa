using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class EmteaTypeGroupDto
    {
        public int Id { get; set; }
        public DateTime AddedTime { get; set; }
        public string EmteaTypeGroupName { get; set; }
        public string EmteaGroupName { get; set; }
        public string EmteaTypeName { get; set; }
        public string EmteaName { get; set; }
        public string EmteaCode { get; set; }
        public int EmteaId { get; set; }
        public int EmteaGroupId { get; set; }
        public int EmteaTypeId { get; set; }
    }
}
