using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class SubeFormDataWDataInput
    {
        public string SubeName { get; set; } 
        public string SubeCode { get; set; } 
        public int Id { get; set; } 
        public string BolgeName { get; set; } 
        public string Cities { get; set; }
        public string HaveDataCities { get; set; }
        public string AddedDate { get; set; }
        public bool IsHavaData { get; set; } 
        public int IsHaveDataCount { get; set; }
    }
}
