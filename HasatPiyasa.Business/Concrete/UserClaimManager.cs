using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class UserClaimManager : IUserClaimService
    {
        private IUserClaimDal _userClaimDal;

        public UserClaimManager(IUserClaimDal userClaimDal)
        {
            _userClaimDal = userClaimDal;
        }

        public async Task<NIslemSonuc<UserClaims>> CreateUserClaim(UserClaims userClaim)
        {
            try
            {
                var addeduserclaim = await _userClaimDal.AddAsync(userClaim);

                return new NIslemSonuc<UserClaims>
                {
                    BasariliMi = true,
                    Veri = addeduserclaim,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<UserClaims>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<UserClaims> GetUserClaim(int id)
        {
            try
            {
                return new NIslemSonuc<UserClaims>
                {
                    BasariliMi = true,
                    Veri = _userClaimDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<UserClaims>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<UserClaims>> GetUserClaimTable(int value)
        {
            try
            {
                var res = await _userClaimDal.GetTable();

                return new NIslemSonuc<UserClaims>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.User).Include(x => x.Claim).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<UserClaims>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<UserClaims>> ListUserClaims()
        {
            try
            {
                return new NIslemSonuc<List<UserClaims>>
                {
                    BasariliMi = true,
                    Veri = _userClaimDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<UserClaims>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<UserClaims>> UpdateUserClaim(UserClaims userClaim)
        {
            try
            {
                var updateduserclaim = await _userClaimDal.UpdateAsync(userClaim);

                return new NIslemSonuc<UserClaims>
                {
                    BasariliMi = true,
                    Veri = updateduserclaim,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<UserClaims>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}
