using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Dtos;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IUserService
    {
        NIslemSonuc<Users> GetUser(int id);
        Task<Users> GetUserName(int id);
        Task<Users> GetUserName(string domainname);
        NIslemSonuc<bool> UserExitsDomain(UserForLoginDto userForLoginDto);
    }
}
