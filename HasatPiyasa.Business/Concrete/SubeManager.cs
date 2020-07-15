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
    public class SubeManager : ISubeService
    {
        private ISubeDal _subeDal;

        public SubeManager(ISubeDal subeDal)
        {
            _subeDal = subeDal;
        }

        public async Task<NIslemSonuc<Subes>> CreateSube(Subes sube)
        {
            try
            {
                var addedsube = await _subeDal.AddAsync(sube);

                return new NIslemSonuc<Subes>
                {
                    BasariliMi = true,
                    Veri = addedsube,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<Subes> GetSube(int id)
        {
            try
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = true,
                    Veri = _subeDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Subes>> GetSubeTable(int value)
        {
            try
            {
                var res = await _subeDal.GetTable();

                return new NIslemSonuc<Subes>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x=>x.Tuiks).Include(x=>x.Bolge).Include(x=>x.Cities).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Subes>> ListAllSubes()
        {
            try
            {
                return new NIslemSonuc<List<Subes>>
                {
                    BasariliMi = true,
                    Veri = _subeDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Subes>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Subes>> UpdateSube(Subes sube)
        {
            try
            {
                var updatedsube = await _subeDal.UpdateAsync(sube);

                return new NIslemSonuc<Subes>
                {
                    BasariliMi = true,
                    Veri = updatedsube,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}
