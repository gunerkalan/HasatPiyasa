using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Dtos;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<NIslemSonuc<Users>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheckServer = await _userService.GetUserName(userForLoginDto.Name);
            if (userToCheckServer == null)
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorDatabaseLogin
                };
            }

            var usertoChechDomain = _userService.UserExitsDomain(userForLoginDto);
            if (!usertoChechDomain.BasariliMi)
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = false,
                    Mesaj = usertoChechDomain.Mesaj
                };
            }

            return new NIslemSonuc<Users>
            {
                BasariliMi = true,
                Veri = userToCheckServer,
                Mesaj = Messages.SusccesfulyLogin
            };
        }

        public async Task<NIslemSonuc<Users>> Profil(int id)
        {
            var user = await _userService.GetUserName(id);
            if (user == null)
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorDatabaseLogin
                };
            }

            return new NIslemSonuc<Users>
            {
                BasariliMi = true,
                Veri = user
            };
        }
    }
}
