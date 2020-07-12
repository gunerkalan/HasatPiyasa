using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class EmteaTypeGroupManager : IEmteaTypeGroupService
    {
        private IEmteaTypeGroupDal _emteaTypeGroupDal;

        public EmteaTypeGroupManager(IEmteaTypeGroupDal emteaTypeGroupDal)
        {
            _emteaTypeGroupDal = emteaTypeGroupDal;
        }
        public async Task<NIslemSonuc<EmteaTypeGroups>> CreateEmteaTypeGroups(EmteaTypeGroups emteatypegroup)
        {
            try
            {
                var addedemteatypegroup = await _emteaTypeGroupDal.AddAsync(emteatypegroup);

                return new NIslemSonuc<EmteaTypeGroups>
                {
                    BasariliMi = true,
                    Veri = addedemteatypegroup,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaTypeGroups>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<EmteaTypeGroups> GetEmteaTypeGroup(int id)
        {
            try
            {
                return new NIslemSonuc<EmteaTypeGroups>
                {
                    BasariliMi = true,
                    Veri = _emteaTypeGroupDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaTypeGroups>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<EmteaTypeGroups>> ListAllEmteTypeGroups()
        {
             try
            {
                return new NIslemSonuc<List<EmteaTypeGroups>>
                {
                    BasariliMi = true,
                    Veri = _emteaTypeGroupDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<EmteaTypeGroups>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<EmteaTypeGroups>> UpdateEmteaTypeGroup(EmteaTypeGroups emteatypegroup)
        {
            try
            {
                var updatedemteatypegroup = await _emteaTypeGroupDal.UpdateAsync(emteatypegroup);

                return new NIslemSonuc<EmteaTypeGroups>
                {
                    BasariliMi = true,
                    Veri = updatedemteatypegroup,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaTypeGroups>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}
