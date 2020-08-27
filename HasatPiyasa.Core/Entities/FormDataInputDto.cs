using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class FormDataInputDto
    {
        public int Id { get; set; }
        public DateTime AddedTime { get; set; }
        public bool IsLock { get; set; }
        public bool IsActive { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int SubeId { get; set; }
        public string SubeName { get; set; }
        public string SubeCode { get; set; }
        public int EmteaId { get; set; }
        public string EmteaName { get; set; }
        public string EmteaCode { get; set; }
    }
}
