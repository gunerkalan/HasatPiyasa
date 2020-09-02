using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
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
    public class UserRoleManager : IUserRoleService
    {
        private IUserRoleDal _userRoleDal;

        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }
        public async Task<NIslemSonuc<List<UserRoles>>> GetUserRoleGTable()
        {
            try
            {
                var res = await _userRoleDal.GetTable();
                var model = res.Where(x => x.IsActive).ToList();


                return new NIslemSonuc<List<UserRoles>>
                {
                    BasariliMi = false,
                    Veri = model.ToList()
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<UserRoles>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
    }
}
