using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Entities
{
    public class UserDto
    {
        public int UserId { get; set; }
        public DateTime AddedTime { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DomainUserName { get; set; }
        public string SubeName { get; set; }
        public string SicilNumber { get; set; }
        public string Title { get; set; }
    }
}
