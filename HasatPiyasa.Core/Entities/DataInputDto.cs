using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class DataInputDto
    {
        public int Id { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsActive { get; set; }
        public int SubeId { get; set; }
        public string SubeName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int EmteaId { get; set; }
        public string EmteaName { get; set; }
        public int EmteaGroupId { get; set; }
        public string EmteaGroupName { get; set; }
        public int EmteaTypeId { get; set; }
        public string EmteaTypeName { get; set; }
        public int AddUserId { get; set; }
        public string AddUserName { get; set; }
        public int UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public int AlimYear { get; set; }
        public double TuikValue { get; set; }
        public double GuessValue { get; set; }
        public double HasatOran { get; set; }
        public double HasatMiktar { get; set; }
        public double UreticiKalanMiktar { get; set; }

    }
}
