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
    public class SubeCityManager : ISubeCityService
    {
        private ISubeCityDal _subeCityDal;

        public SubeCityManager(ISubeCityDal subeCityDal)
        {
            _subeCityDal = subeCityDal;
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
