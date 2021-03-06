﻿using DevExpress.Data.ODataLinq.Helpers;
using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Business;
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

        public async Task<NIslemSonuc<Users>> GetUserTable(string username)
        {
            try
            {
                var res = await _userDal.GetTable();

                var response = res.AsQueryable().Include(x => x.UserClaims).Include(x => x.UserRole).Include(x => x.Sube).ThenInclude(x => x.SubeCities).ThenInclude(x => x.City).Where(x => x.IsActive && x.DomainUserName == username).ToList().FirstOrDefault();

                if(response!=null)
                {
                    return new NIslemSonuc<Users>
                    {
                        BasariliMi = true,
                        Veri = response
                    };
                }
                else
                {
                    return new NIslemSonuc<Users>
                    {
                        BasariliMi = false,
                        Mesaj = "Hatalı Giriş, Lütfen tekrar deneyiniz !"
                    };
                }

                
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

        public async Task<NIslemSonuc<List<UserDto>>> GetUserGTable()
        {
            try
            {
                var res = await _userDal.GetTable();
                var model = res.Include(x => x.Sube).Include(x=>x.UserRole).Where(x => x.IsActive).ToList();


                var response = model.Select(x => new UserDto
                {
                    AddedTime = x.AddedTime,
                    DomainUserName = x.DomainUserName,
                    Name = x.Name,
                    SicilNumber = x.SicilNumber,
                    SubeName = x.Sube.SubeName,
                    Surname = x.Surname,
                    Title = x.Title,
                    UserId = x.UserId,
                    UserRole = x.UserRole.Role,
                    SubeId = x.SubeId,
                    UserRoleId = x.UserRoleId

                }).ToList();

                return new NIslemSonuc<List<UserDto>>
                {
                    BasariliMi = false,
                    Veri = response
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<UserDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Users>> CreateUser(Users user)
        {
            try
            {
                NIslemSonuc sonuc = BusinessRules.Run(CheckDomainUserExists(user.DomainUserName));
                if (sonuc.BasariliMi)
                {
                    var addeduser = await _userDal.AddAsync(user);

                    return new NIslemSonuc<Users>
                    {
                        BasariliMi = true,
                        Veri = addeduser,
                        Mesaj = Messages.SuccessfulyAddUser
                    };
                }
                else
                {
                    return new NIslemSonuc<Users>
                    {
                        BasariliMi = false,
                        Mesaj = sonuc.Mesaj
                    };
                }


            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        private NIslemSonuc<bool> CheckDomainUserExists(string domainname)
        {
            if (_userDal.Get(p => p.DomainUserName == domainname) != null)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorDomainUser
                };
            }
            return new NIslemSonuc<bool>
            {
                BasariliMi = true
            };
        }

        public async Task<NIslemSonuc<UserDto>> GetUserAsync(int id)
        {
            try
            {
                var res = await _userDal.GetTable();
                var model = res.Include(x => x.UserRole).Include(x => x.Sube).AsNoTracking().FirstOrDefault(x => x.UserId == id && x.IsActive);

                var response = (new UserDto
                {
                    UserId= model.UserId,
                    AddedTime = model.AddedTime,
                    DomainUserName = model.DomainUserName,
                    Name = model.Name,
                    Surname = model.Surname,
                    SicilNumber = model.SicilNumber,
                    SubeName = model.Sube.SubeName,
                    Title = model.Title,
                    UserRole = model.UserRole.Role,
                    SubeId = model.SubeId,
                    UserRoleId = model.UserRoleId,
                    Email = model.Email
                });

                return new NIslemSonuc<UserDto>
                {
                    BasariliMi = true,
                    Veri = response
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<UserDto>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Users>> UpdateUser(Users user)
        {
            try
            {
                var updateduser = await _userDal.UpdateAsync(user);

                return new NIslemSonuc<Users>
                {
                    BasariliMi = true,
                    Veri = updateduser,
                    Mesaj = Messages.UserUpdate
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Users>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<bool>> DeleteUser(Users user)
        {
            try
            {
                var deleteduser = await _userDal.DeleteSoftAsync(user);

                return new NIslemSonuc<bool>
                {
                    BasariliMi = true,
                    Veri = deleteduser,
                    Mesaj = Messages.UserDelete

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.InnerException.Message
                };
            }
        }
    }
}
