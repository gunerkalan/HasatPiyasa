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
    public class BolgeManager : IBolgeService
    {
        private IBolgeDal _bolgeDal;

        public BolgeManager(IBolgeDal bolgeDal)
        {
            _bolgeDal = bolgeDal;
        }

        public async Task<NIslemSonuc<Bolges>> CreateBolge(Bolges bolges)
        {
            try
            {
                var addedbolge = await _bolgeDal.AddAsync(bolges);

                return new NIslemSonuc<Bolges>
                {
                    BasariliMi = true,
                    Veri = addedbolge,
                   
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Bolges>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<Bolges> GetBolge(int id)
        {
            try
            {

                return new NIslemSonuc<Bolges>
                {
                    BasariliMi = true,
                    Veri = _bolgeDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Bolges>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Bolges>> GetBolgeTable(int value)
        {
            try
            {
                var res = await _bolgeDal.GetTable();

                return new NIslemSonuc<Bolges>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.Subes).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Bolges>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Bolges>> ListAllBolges()
        {
            try
            {
                return new NIslemSonuc<List<Bolges>>
                {
                    BasariliMi = true,
                    Veri = _bolgeDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Bolges>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Bolges>> UpdateBolges(Bolges bolges)
        {
            try
            {
                var updatedbolge = await _bolgeDal.UpdateAsync(bolges);

                return new NIslemSonuc<Bolges>
                {
                    BasariliMi = true,
                    Veri = updatedbolge,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Bolges>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}
