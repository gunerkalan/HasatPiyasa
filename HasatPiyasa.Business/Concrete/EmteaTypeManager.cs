using DevExpress.Utils.OAuth;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class EmteaTypeManager : IEmteaTypeService
    {
        private IEmteaTypeDal _emteaTypeDal;

        public EmteaTypeManager(IEmteaTypeDal emteaTypeDal)
        {
            _emteaTypeDal = emteaTypeDal;
        }
        public async Task<NIslemSonuc<EmteaTypes>> CreateEmteaType(EmteaTypes emteatype)
        {
           
            try
            {
                NIslemSonuc sonuc = BusinessRules.Run(CheckEmteaTypeNameExists(emteatype.EmteaTypeCode));
                if (sonuc.BasariliMi)
                {
                    var addedemteatype = await _emteaTypeDal.AddAsync(emteatype);

                    return new NIslemSonuc<EmteaTypes>
                    {
                        BasariliMi = true,
                        Veri = addedemteatype,
                        Mesaj = Messages.SuccessfulyAddEmteaType

                    };
                }
                else
                {
                    return new NIslemSonuc<EmteaTypes>
                    {
                        BasariliMi = false,
                        Mesaj = sonuc.Mesaj
                    };

                }
               
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaTypes>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        private NIslemSonuc<bool> CheckEmteaTypeNameExists(string emteatypename)
        {
            if (_emteaTypeDal.Get(p => p.EmteaTypeCode == emteatypename) != null)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorEmteaTypeName
                };
            }
            return new NIslemSonuc<bool>
            {
                BasariliMi = true
            };
        }

        public async Task<NIslemSonuc<List<EmteaTypeDto>>> GetEmteaTypeGTable()
        {
            try
            {
                var res = await _emteaTypeDal.GetTable();
                var model = res.Include(x => x.EmteaGroup).ThenInclude(x => x.Emtea).Where(x=>x.IsActive).ToList();
                
               
                var response = model.Select(x => new EmteaTypeDto
                {
                    EmteaCode = x.EmteaGroup.Emtea.EmteaCode,
                    EmteaName = x.EmteaGroup.Emtea.EmteaName,
                    EmteaTypeId = x.Id,
                    EmteaTypeName = x.EmteaTypeName,
                    GroupName = x.EmteaGroup.GroupName,
                    AddedTime = x.AddedTime,
                    EmteaId = x.EmteaGroup.Emtea.Id,
                    EmteaGroupId = x.EmteaGroupId,
                    EmteaTypeCode = x.EmteaTypeCode

                }).ToList();
                
                return new NIslemSonuc<List<EmteaTypeDto>>
                {
                    BasariliMi = false,
                    Veri = response
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<EmteaTypeDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<EmteaTypes> GetEmteaType(int id)
        {
            try
            {
                return new NIslemSonuc<EmteaTypes>
                {
                    BasariliMi = true,
                    Veri = _emteaTypeDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaTypes>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<EmteaTypes>> GetEmteaTypeTable(int value)
        {
            try
            {
                var res = await _emteaTypeDal.GetTable();

                return new NIslemSonuc<EmteaTypes>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.EmteaTypeGroups).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaTypes>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

  
        public NIslemSonuc<List<EmteaTypes>> ListAllEmteType()
        {
            try
            {
                return new NIslemSonuc<List<EmteaTypes>>
                {
                    BasariliMi = true,
                    Veri = _emteaTypeDal.GetList(u=>u.IsActive).ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<EmteaTypes>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<EmteaTypes>> UpdateEmteaType(EmteaTypes emteatype)
        {
            try
            {
                var updatedemteatype = await _emteaTypeDal.UpdateAsync(emteatype);

                return new NIslemSonuc<EmteaTypes>
                {
                    BasariliMi = true,
                    Veri = updatedemteatype,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaTypes>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<EmteaTypeEditDto>> GetEmteaTypesAsync(int id)
        {
            try
            {
                var res = await _emteaTypeDal.GetTable();
                var model = res.Include(x => x.EmteaGroup).ThenInclude(x => x.Emtea).FirstOrDefault(x=>x.Id==id && x.IsActive);

                var response = (new EmteaTypeEditDto
                {
                    EmteaId = model.EmteaGroup.Emtea.Id,
                    EmteaGroupId = model.EmteaGroupId,
                    EmteaTypeName = model.EmteaTypeName,
                    EmteaTypeCode = model.EmteaTypeCode
                });

                return new NIslemSonuc<EmteaTypeEditDto>
                {
                    BasariliMi = true,
                    Veri = response
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaTypeEditDto>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<bool>> DeleteEmteaType(EmteaTypes emteatype)
        {
            try
            {
                var deletedemteatype = await _emteaTypeDal.DeleteSoftAsync(emteatype);

                return new NIslemSonuc<bool>
                {
                    BasariliMi = true,
                    Veri = deletedemteatype,
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

        public async Task<NIslemSonuc<List<EmteaTypes>>> GetEmteaTypesForEmtea(int EmteaId)
        {
            try
            {
                var res = await _emteaTypeDal.GetTable();

                return new NIslemSonuc<List<EmteaTypes>>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.EmteaGroup).ThenInclude(x=>x.Emtea).Where(x => x.EmteaGroup.Emtea.Id == EmteaId).ToList()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<EmteaTypes>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
    }
}
