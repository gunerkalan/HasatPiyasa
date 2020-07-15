using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Dtos;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IAuthService
    {
        Task<NIslemSonuc<Users>> Login(UserForLoginDto userForLoginDto);
        Task<NIslemSonuc<Users>> Profil(int id);
    }
}
