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
    public class CaptionManager : ICaptionService
    {
        private ICaptionDal _captionDal;

        public CaptionManager(ICaptionDal captionDal)
        {
            _captionDal = captionDal;
        }
        public async Task<NIslemSonuc<Captions>> CreateCaption(Captions caption)
        {
            try
            {
                var addedcaption = await _captionDal.AddAsync(caption);

                return new NIslemSonuc<Captions>
                {
                    BasariliMi = true,
                    Veri = addedcaption,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Captions>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<Captions> GetCaption(int id)
        {
            try
            {
                return new NIslemSonuc<Captions>
                {
                    BasariliMi = true,
                    Veri = _captionDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Captions>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Captions>> GetCaptionTable(int value)
        {
            try
            {
                var res = await _captionDal.GetTable();

                return new NIslemSonuc<Captions>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.EmteaTypes).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Captions>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Captions>> ListAllCaptions()
        {
            try
            {
                return new NIslemSonuc<List<Captions>>
                {
                    BasariliMi = true,
                    Veri = _captionDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Captions>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Captions>> UpdateCaption(Captions emtea)
        {
            try
            {
                var updatedcaption = await _captionDal.UpdateAsync(emtea);

                return new NIslemSonuc<Captions>
                {
                    BasariliMi = true,
                    Veri = updatedcaption,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Captions>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}
