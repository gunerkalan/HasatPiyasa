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
    public class TuikManager : ITuikService
    {
        private ITuikDal _tuikDal;

        public TuikManager(ITuikDal tuikDal)
        {
            _tuikDal = tuikDal;
        }

        public async Task<NIslemSonuc<Tuiks>> CreateTuikData(Tuiks tuik)
        {
            try
            {
                var addedtuikdata = await _tuikDal.AddAsync(tuik);

                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = true,
                    Veri = addedtuikdata,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<Tuiks> GetTuikData(int id)
        {
            try
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = true,
                    Veri = _tuikDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Tuiks>> GetTuikTable(int value)
        {
            try
            {
                var res = await _tuikDal.GetTable();

                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.City).Include(x=>x.EmteaType).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Tuiks>> ListAllTuikData()
        {
            try
            {
                return new NIslemSonuc<List<Tuiks>>
                {
                    BasariliMi = true,
                    Veri = _tuikDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Tuiks>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Tuiks>> UpdateTuikData(Tuiks tuik)
        {
            try
            {
                var updatedtuikdata = await _tuikDal.UpdateAsync(tuik);

                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = true,
                    Veri = updatedtuikdata,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}
