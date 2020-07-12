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
    public class EmteaGrupManager : IEmteaGroupService
    {
        private IEmteaGroupDal _emteaGrupDal;

        public EmteaGrupManager(IEmteaGroupDal emteaGroupDal)
        {
            _emteaGrupDal = emteaGroupDal;
        }

        public async Task<NIslemSonuc<EmteaGroups>> CreateEmteaGroup(EmteaGroups emteaGroups)
        {
            try
            {
                var addedemteagroup = await _emteaGrupDal.AddAsync(emteaGroups);

                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = true,
                    Veri = addedemteagroup,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<EmteaGroups> GetEmteaGroup(int id)
        {
            try
            {
                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = true,
                    Veri = _emteaGrupDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<EmteaGroups>> ListAllEmteaGroups()
        {
            try
            {
                return new NIslemSonuc<List<EmteaGroups>>
                {
                    BasariliMi = true,
                    Veri = _emteaGrupDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<EmteaGroups>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<EmteaGroups>> UpdateEmteaGroup(EmteaGroups emteaGroup)
        {
            try
            {
                var updatedemteagroup = await _emteaGrupDal.UpdateAsync(emteaGroup);

                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = true,
                    Veri = updatedemteagroup,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}
