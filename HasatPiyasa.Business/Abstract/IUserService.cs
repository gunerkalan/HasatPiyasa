using HasatPiyasa.Core.Entities;
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
        Task<NIslemSonuc<Users>> GetUserTable(string username);
        Task<NIslemSonuc<List<UserDto>>> GetUserGTable();
        Task<NIslemSonuc<Users>> CreateUser(Users user);
        Task<NIslemSonuc<UserDto>> GetUserAsync(int id);
        Task<NIslemSonuc<Users>> UpdateUser(Users user);
        Task<NIslemSonuc<bool>> DeleteUser(Users user);
    }
}
