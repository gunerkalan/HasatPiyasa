using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Business;
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
    public class EmteaManager : IEmteaService
    {
        private IEmteaDal _emteaDal;

        public EmteaManager(IEmteaDal emteaDal)
        {
            _emteaDal = emteaDal;
        }

        public async Task<NIslemSonuc<Emteas>> CreateEmtea(Emteas emtea)
        {
            try
            {
                NIslemSonuc sonuc = BusinessRules.Run(CheckEmteaCodeExists(emtea.EmteaCode));
                if(sonuc.BasariliMi)
                {
                    var addedemtea = await _emteaDal.AddAsync(emtea);

                    return new NIslemSonuc<Emteas>
                    {
                        BasariliMi = true,
                        Veri = addedemtea,
                        Mesaj = Messages.SuccessfulyAddEmtea
                    };
                }
                else
                {
                    return new NIslemSonuc<Emteas>
                    {
                        BasariliMi=false,
                        Mesaj = Messages.ErrorEmteaCode
                    };
                }

              
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        private NIslemSonuc<bool> CheckEmteaCodeExists(string emteacode)
        {
            if(_emteaDal.Get(p=>p.EmteaCode==emteacode) != null)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder
                };
            }
            return new NIslemSonuc<bool>
            {
                BasariliMi=true
            };
        }

        public NIslemSonuc<Emteas> GetEmtea(int id)
        {
            try
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = true,
                    Veri = _emteaDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Emteas>> GetEmteaTable(int value,int cityId)
        {
            try
            {
                var res = await _emteaDal.GetTable();

                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.EmteaGroups).ThenInclude(x => x.EmteaTypes).ThenInclude(x=>x.EmteaTypeGroups).ThenInclude(x=>x.EmteaType.Tuiks).Include(x=>x.EmteaGroups).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Emteas>> ListAllEmteas()
        {
            try
            {
                return new NIslemSonuc<List<Emteas>>
                {
                    BasariliMi = true,
                    Veri = _emteaDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Emteas>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Emteas>> UpdateEmtea(Emteas emtea)
        {
            try
            {
                var updatedemtea = await _emteaDal.UpdateAsync(emtea);

                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = true,
                    Veri = updatedemtea,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<EmteaDto>>> GetEmteaGTable()
        {
            try
            {
                var res = await _emteaDal.GetTable();
                var model = res.AsNoTracking().Include(x => x.EmteaGroups).Where(x => x.IsActive).ToList();


                var response = model.Select(x => new EmteaDto
                {
                    Id = x.Id,
                    EmteaCode = x.EmteaCode,
                    EmteaName = x.EmteaName,
                    AddedTime = x.AddedTime

                }).ToList();

                return new NIslemSonuc<List<EmteaDto>>
                {
                    BasariliMi = false,
                    Veri = response
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<EmteaDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
    }
}
