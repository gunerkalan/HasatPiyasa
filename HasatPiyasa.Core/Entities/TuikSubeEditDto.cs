﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class TuikSubeEditDto
    {
        public int Id { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int TuikYear { get; set; }
        public string SubeName { get; set; }
        public string EmteaTypeName { get; set; }
        public string AddedUser { get; set; }
        public string EmteaGroupName { get; set; }
        public string EmteaName { get; set; }
        public string CityName { get; set; }
        public double TuikValue { get; set; }
        public string EmteaCode { get; set; }
        public int GuessYear { get; set; }
        public double GuessValue { get; set; }
        public bool IsCity { get; set; }
        public string UpdatedUser { get; set; }
        public string AddSicil { get; set; }
        public string UpdateSicil { get; set; }
        public int EmteaId { get; set; }
        public int EmteaGroupId { get; set; }
        public int? SubeId { get; set; }
        public int? CityId { get; set; }
        public int EmteaTypeId { get; set; }
        public int UserId { get; set; }
    }
}
