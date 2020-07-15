﻿using DevExpress.Data.ODataLinq.Helpers;
using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Dtos;
using HasatPiyasa.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
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

        public async Task<NIslemSonuc<Users>> GetUserTable(int value)
        {
            try
            {
                var res = await _userDal.GetTable();

                return new NIslemSonuc<Users>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.UserClaims).Where(x => x.UserId == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<bool> UserExitsDomain(UserForLoginDto userForLoginDto)
        {
            Guid anahtarGUID;
            
            var result = ValidateCredentials(userForLoginDto.UserName, userForLoginDto.Password);

            //DirectoryEntry deAD = new DirectoryEntry(cnf, resultText, userForLoginDto.Password, AuthenticationTypes.Secure | AuthenticationTypes.None);

            try
            {
               // anahtarGUID = deAD.Guid;
                return new NIslemSonuc<bool>
                {
                    BasariliMi = true,

                };
            }
            catch (Exception ex)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorDomainLogin,
                };
            }
        }

        public static bool ValidateCredentials(string userName, string password)
        {
            try
            {
                using (var adContext = new PrincipalContext(ContextType.Domain, "LDAP://tmo.local"))
                {
                    return adContext.ValidateCredentials(userName, password);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}