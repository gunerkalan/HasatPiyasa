using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class SubeCityDto: IEquatable<SubeCityDto>
    {
        public int SubeId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string SubeName { get; set; }

        public bool Equals([AllowNull] SubeCityDto other)
        {
            if (Object.ReferenceEquals(other, null)) return false;


            if (Object.ReferenceEquals(this, other)) return true;


            return SubeId.Equals(other.SubeId);
        }

        public override int GetHashCode()
        {
            
            int hash = SubeId.GetHashCode();

            return hash;
        }
    }
}
