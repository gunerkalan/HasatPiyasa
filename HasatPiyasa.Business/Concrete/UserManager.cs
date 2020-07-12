using DevExpress.Data.ODataLinq.Helpers;
using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Dtos;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public NIslemSonuc<Users> GetUser(int id)
        {
            try
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = true,
                    Veri = _userDal.Get(u => u.UserId == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = false,
                    Mesaj = hata.Message
                };
            }
        }

        public async Task<Users> GetUserName(int id)
        {
            var user = await _userDal.GetTable();
            return user.FirstOrDefault(x => x.UserId == id);
        }

        public async Task<Users> GetUserName(string domainname)
        {
            var user = await _userDal.GetTable();
            return user.FirstOrDefault(x => x.DomainUserName == domainname);
        }

        public NIslemSonuc<bool> UserExitsDomain(UserForLoginDto userForLoginDto)
        {
            Guid anahtarGUID;
            string resultText = userForLoginDto.Name;
            DirectoryEntry deAD = new DirectoryEntry(ConfigurationManager.AppSettings["LDAP"], resultText, userForLoginDto.Password, AuthenticationTypes.Secure | AuthenticationTypes.None);

            try
            {
                anahtarGUID = deAD.Guid;
                return new NIslemSonuc<bool>
                {
                    BasariliMi = true,

                };
            }
            catch (Exception)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorDomainLogin,
                };
            }
        }
    }
}
