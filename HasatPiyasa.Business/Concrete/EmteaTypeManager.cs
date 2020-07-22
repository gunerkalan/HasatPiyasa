using DevExpress.Utils.OAuth;
using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Entities;
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
                var addedemteatype = await _emteaTypeDal.AddAsync(emteatype);

                return new NIslemSonuc<EmteaTypes>
                {
                    BasariliMi = true,
                    Veri = addedemteatype,

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
                    AddedTime = x.AddedTime

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
                    Veri = _emteaTypeDal.GetList().ToList()
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
    }
}
