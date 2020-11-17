using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Dtos;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private const string DisplayNameAttribute = "DisplayName";
        private const string SAMAccountNameAttribute = "SAMAccountName";
        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<NIslemSonuc<Users>> Login(UserForLoginDto userForLoginDto)
        {
            var res = await _userService.GetUserTable(userForLoginDto.UserName);

            if(res.BasariliMi)
            {
                var isdomain = res.Veri.IsDomain;
                var password = res.Veri.Password;

                if(isdomain)
                {
                    if (LdapAuth(userForLoginDto).BasariliMi)
                    {
                        return new NIslemSonuc<Users>
                        {
                            BasariliMi = true,
                            Veri = res.Veri,
                            Mesaj = Messages.SusccesfulyLogin
                        };
                    }
                    else
                    {
                        return new NIslemSonuc<Users>
                        {
                            BasariliMi = false,
                            Mesaj = Messages.ErrorDomainLogin

                        };
                    }
                }
                else
                {
                    if(password==userForLoginDto.Password)
                    {
                        return new NIslemSonuc<Users>
                        {
                            BasariliMi = true,
                            Veri = res.Veri,
                            Mesaj = Messages.SusccesfulyLogin
                        };
                    }
                    else
                    {
                        return new NIslemSonuc<Users>
                        {
                            BasariliMi = false,
                            Mesaj = Messages.ErrorDomainLogin

                        };
                    }
                }
            }
            else
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = false,
                    Mesaj = res.Mesaj

                };
            }
           
        }
        public NIslemSonuc<bool> LdapAuth(UserForLoginDto userForLoginDto)
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(ConfigurationManager.AppSettings["LDAP"], userForLoginDto.UserName, userForLoginDto.Password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userForLoginDto.UserName);
                        searcher.PropertiesToLoad.Add(DisplayNameAttribute);
                        searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            var displayName = result.Properties[DisplayNameAttribute];
                            var samAccountName = result.Properties[SAMAccountNameAttribute];

                            return new NIslemSonuc<bool>
                            {
                                BasariliMi = true,                               
                            };
                        }
                        else
                        {
                            return new NIslemSonuc<bool>
                            {
                                BasariliMi = false,
                                Mesaj = "Lütfen Kullanıcı adınızı veya şifrenizi kontrol ediniz !",
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if we get an error, it means we have a login failure.
                // Log specific exception
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = "Lütfen Kullanıcı adınızı veya şifrenizi kontrol ediniz !",
                    //ErrorMessage = ex.InnerException.Message
                };
            }
           
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
