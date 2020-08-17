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
    public class EmteaGrupManager : IEmteaGroupService
    {
        private IEmteaGroupDal _emteaGrupDal;

        public EmteaGrupManager(IEmteaGroupDal emteaGroupDal)
        {
            _emteaGrupDal = emteaGroupDal;
        }

        public async Task<NIslemSonuc<EmteaGroups>> CreateEmteaGroup(EmteaGroups emteaGroups)
        {

            NIslemSonuc sonuc = BusinessRules.Run(CheckEmteaGroupNameExists(emteaGroups.GroupName));
            if(sonuc.BasariliMi)
            {
                try
                {
                    var addedemteagroup = await _emteaGrupDal.AddAsync(emteaGroups);

                    return new NIslemSonuc<EmteaGroups>
                    {
                        BasariliMi = true,
                        Veri = addedemteagroup,
                        Mesaj = Messages.SuccessfulyAddEmteaGruo

                    };
                }
                catch (Exception hata)
                {
                    return new NIslemSonuc<EmteaGroups>
                    {
                        BasariliMi = false,
                        Mesaj = Messages.ErrorAdd,
                        ErrorMessage = hata.InnerException.Message
                    };
                }
            }
            else
            {
                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorEmteaGroupName
                };
            }
           
        }

        private NIslemSonuc<bool> CheckEmteaGroupNameExists(string emteagoupname)
        {
            if (_emteaGrupDal.Get(p => p.GroupName == emteagoupname) != null)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorEmteaGroupName
                };
            }
            return new NIslemSonuc<bool>
            {
                BasariliMi = true
            };
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

        public async Task<NIslemSonuc<List<EmteaGrupDto>>> GetEmteaGroupTable()
        {
            try
            {
                var res = await _emteaGrupDal.GetTable();
                var model = res.Include(x => x.Emtea).Where(u=>u.IsActive).ToList();
                var response = model.Select(x=> new EmteaGrupDto
                {
                    AddedTime = x.AddedTime,
                    EmteaGrupAd = x.GroupName,
                    Id = x.Id,
                    EmteaKod = x.Emtea.EmteaCode,
                    EmteaName = x.Emtea.EmteaName
                }).ToList();

                return new NIslemSonuc<List<EmteaGrupDto>>
                {
                    BasariliMi = true,
                    Veri = response
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<EmteaGrupDto>>
                {
                    BasariliMi=false,
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
                    Veri = _emteaGrupDal.GetList(u=>u.IsActive).ToList()
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
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<EmteaGroups>> GetEmteaGroupAsync(int id)
        {
            try
            {
                var res = await _emteaGrupDal.GetTable();
                var model = res.FirstOrDefault(x => x.Id == id);

                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = true,
                    Veri = model
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaGroups>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<bool>> DeleteEmteaGroup(EmteaGroups emteagroup)
        {
            try
            {
                var deletedemteagroup = await _emteaGrupDal.DeleteSoftAsync(emteagroup);

                return new NIslemSonuc<bool>
                {
                    BasariliMi = true,
                    Veri = deletedemteagroup,
                    Mesaj = Messages.EmteaGroupDelete

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.InnerException.Message
                };
            }
        }
    }
}
