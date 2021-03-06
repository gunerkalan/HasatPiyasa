﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class EmteaTypeDto
    {
        public int EmteaTypeId { get; set; }
        public string EmteaTypeName { get; set; }
        public string EmteaName { get; set; }
        public string EmteaCode { get; set; }
        public string EmteaTypeCode { get; set; }
        public string GroupName { get; set; }
        public DateTime AddedTime { get; set; }
        public int EmteaId { get; set; }
        public int EmteaGroupId { get; set; }
    }
}
