using HasastPiyasa.DataAccess.Abstract;
using HasastPiyasa.DataAccess.Concrete;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Core.Entities;
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
    public class SubeCityManager : EfSubeCityDal, ISubeCityService 
    {
        private ISubeCityDal _subeCityDal;

        public SubeCityManager(ISubeCityDal subeCityDal)
        {
            _subeCityDal = subeCityDal;
        }

        public async Task<NIslemSonuc<List<SubeCityDto>>> GetSbCityGTable()
        {
            try
            {
                var res = await _subeCityDal.GetTable();
                var model = res.Include(x => x.Sube).Include(x => x.City).Where(x => x.IsActive).ToList();

                var response = model.Select(x => new SubeCityDto
                {
                    CityId = x.City.Id,
                    SubeId = x.Sube.Id,
                    CityName = x.City.Name,
                    SubeName = x.Sube.SubeName

                }).ToList();

                return new NIslemSonuc<List<SubeCityDto>>
                {
                    BasariliMi = false,
                    Veri = response
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<SubeCityDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<SubeCities>>> GetSubeCityGTable(int id)
        {
            try
            {
                var res = await _subeCityDal.GetTable();
                var model = res.Include(x => x.City).Include(x => x.Sube).Where(x => x.IsActive && x.SubeId==id).ToList();

                
                return new NIslemSonuc<List<SubeCities>>
                {
                    BasariliMi = false,
                    Veri = model
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<SubeCities>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
    }
}
