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
    public class CityManager : ICityService
    {
        private ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public async Task<NIslemSonuc<Cities>> CreateCity(Cities city)
        {
            try
            {
                var addedcity = await _cityDal.AddAsync(city);

                return new NIslemSonuc<Cities>
                {
                    BasariliMi = true,
                    Veri = addedcity,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Cities>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<Cities> GetCity(int id)
        {
            try
            {
                return new NIslemSonuc<Cities>
                {
                    BasariliMi = true,
                    Veri = _cityDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Cities>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Cities>> ListAllCities()
        {
            try
            {
                return new NIslemSonuc<List<Cities>>
                {
                    BasariliMi = true,
                    Veri = _cityDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Cities>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
  

        public async Task<NIslemSonuc<Cities>> UpdateCity(Cities city)
        {
            try
            {
                var updatedcity = await _cityDal.UpdateAsync(city);

                return new NIslemSonuc<Cities>
                {
                    BasariliMi = true,
                    Veri = updatedcity,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Cities>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}

