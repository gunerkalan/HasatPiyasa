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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace HasatPiyasa.Business.Concrete
{
    public class TuikManager : ITuikService
    {
        private ITuikDal _tuikDal;

        public TuikManager(ITuikDal tuikDal)
        {
            _tuikDal = tuikDal;
        }

        public async Task<NIslemSonuc<Tuiks>> CreateTuikData(Tuiks tuik)
        {
            try
            {
                NIslemSonuc sonuc = BusinessRules.Run(CheckTuikSubeDataExists(tuik.GuessYear, tuik.EmteaTypeId, (int)tuik.SubeId));

                if (sonuc.BasariliMi)
                {
                    var addedtuikdata = await _tuikDal.AddAsync(tuik);

                    return new NIslemSonuc<Tuiks>
                    {
                        BasariliMi = true,
                        Veri = addedtuikdata,
                        Mesaj = Messages.TuikSubeAdd
                    };
                }
                else
                {
                    return new NIslemSonuc<Tuiks>
                    {
                        BasariliMi = true,
                        Mesaj = Messages.TuikSubeAddError,
                        
                    };
                }


            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        private NIslemSonuc<bool> CheckTuikSubeDataExists(int year, int emteatypeid, int subeid)
        {

            if (_tuikDal.Get(p => p.GuessYear == year && p.EmteaTypeId == emteatypeid && p.SubeId == subeid && p.IsActive && p.IsCity==false) != null)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddTuikSubeData
                };
            }
            return new NIslemSonuc<bool>
            {
                BasariliMi = true
            };
        }

        private NIslemSonuc<bool> CheckTuikCityDataExists(int year, int emteatypeid, int cityid)
        {
            if (_tuikDal.Get(p => p.GuessYear == year && p.EmteaTypeId == emteatypeid && p.CityId == cityid) != null)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddTuikCityData
                };
            }
            return new NIslemSonuc<bool>
            {
                BasariliMi = true
            };
        }

        public async Task<NIslemSonuc<List<TuikCityDto>>> GetTuikCityGTable()
        {
            try
            {
                var res = await _tuikDal.GetTable();
                var model = res.Include(x => x.City).Include(x => x.Sube).Include(x => x.AddUser).Include(x => x.EmteaType).ThenInclude(x => x.EmteaGroup).ThenInclude(x => x.Emtea).Where(x => x.IsActive && x.IsCity).ToList();

                var response = model.Select(x => new TuikCityDto
                {
                    Id = x.Id,
                    AddedTime = x.AddedTime,
                    AddedUser = x.AddUser.Name + " " + x.AddUser.Surname,
                    EmteaGroupName = x.EmteaType.EmteaGroup.GroupName,
                    EmteaName = x.EmteaType.EmteaGroup.Emtea.EmteaName,
                    EmteaTypeName = x.EmteaType.EmteaTypeName,
                    CityName = x.City.Name,
                    EmteaCode = x.EmteaType.EmteaGroup.Emtea.EmteaCode,
                    TuikValue = x.TuikValue,
                    TuikYear = x.TuikYear,
                    GuessValue = x.GuessValue,
                    GuessYear = x.GuessYear
                }).ToList();

                return new NIslemSonuc<List<TuikCityDto>>
                {
                    BasariliMi = true,
                    Veri = response
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<TuikCityDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<Tuiks> GetTuikData(int id)
        {
            try
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = true,
                    Veri = _tuikDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<TuikSubeDto>>> GetTuikSubeGTable()
        {
            try
            {
                var res = await _tuikDal.GetTable();
                var model = res.Include(x => x.Sube).Include(x => x.City).Include(x => x.AddUser).Include(x => x.EmteaType).ThenInclude(x => x.EmteaGroup).ThenInclude(x => x.Emtea).Where(x => x.IsActive && x.IsCity == false).ToList();

                var response = model.Select(x => new TuikSubeDto
                {
                    Id = x.Id,
                    AddedTime = x.AddedTime,
                    AddedUser = x.AddUser.Name + " " + x.AddUser.Surname,
                    EmteaGroupName = x.EmteaType.EmteaGroup.GroupName,
                    EmteaName = x.EmteaType.EmteaGroup.Emtea.EmteaName,
                    EmteaTypeName = x.EmteaType.EmteaTypeName,
                    SubeName = x.Sube.SubeName,
                    EmteaCode = x.EmteaType.EmteaGroup.Emtea.EmteaCode,
                    TuikValue = x.TuikValue,
                    TuikYear = x.TuikYear,
                    GuessValue = x.GuessValue,
                    GuessYear = x.GuessYear,
                    IsCity = x.IsCity
                }).ToList();

                return new NIslemSonuc<List<TuikSubeDto>>
                {
                    BasariliMi = true,
                    Veri = response
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<TuikSubeDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Tuiks>> GetTuikTable(int value)
        {
            try
            {
                var res = await _tuikDal.GetTable();

                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.City).Include(x => x.EmteaType).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Tuiks>> ListAllTuikData()
        {
            try
            {
                return new NIslemSonuc<List<Tuiks>>
                {
                    BasariliMi = true,
                    Veri = _tuikDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Tuiks>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Tuiks>> UpdateTuikData(Tuiks tuik)
        {
            try
            {
                var updatedtuikdata = await _tuikDal.UpdateAsync(tuik);

                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = true,
                    Veri = updatedtuikdata,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<Tuiks>> CreateTuikDataForCity(Tuiks tuik)
        {
            try
            {
                NIslemSonuc sonuc = BusinessRules.Run(CheckTuikCityDataExists(tuik.GuessYear, tuik.EmteaTypeId, (int)tuik.CityId));

                if (sonuc.BasariliMi)
                {
                    var addedtuikdata = await _tuikDal.AddAsync(tuik);

                    return new NIslemSonuc<Tuiks>
                    {
                        BasariliMi = true,
                        Veri = addedtuikdata,

                    };
                }
                else
                {
                    return new NIslemSonuc<Tuiks>
                    {
                        BasariliMi = true,
                        Mesaj = sonuc.Mesaj,

                    };
                }


            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Tuiks>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<TuikSubeEditDto>> GetTuikSubeAsync(int id)
        {
            try
            {
                var res = await _tuikDal.GetTable();
                var model = res.Include(x => x.City).Include(x=>x.Sube).Include(x=>x.EmteaType).ThenInclude(x => x.EmteaGroup).ThenInclude(x=>x.Emtea).FirstOrDefault(x => x.Id == id && x.IsActive);

                var response = (new TuikSubeEditDto
                {
                    Id = model.Id,
                    
                });

                return new NIslemSonuc<TuikSubeEditDto>
                {
                    BasariliMi = true,
                    Veri = response
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<TuikSubeEditDto>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<bool>> DeleteTuik(Tuiks tuik)
        {
            try
            {
                var deletedtuik = await _tuikDal.DeleteSoftAsync(tuik);

                return new NIslemSonuc<bool>
                {
                    BasariliMi = true,
                    Veri = deletedtuik,
                    Mesaj = Messages.TuikDelete

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

        public async Task<NIslemSonuc<TuikSubeDto>> GetDetail(int id)
        {
            try
            {
                var res = await _tuikDal.GetTable();
                var model = await res.Include(a => a.AddUser)
                    .Include(a => a.City)
                    .Include(a => a.UpdateUser)
                    .Include(a => a.Sube)
                    .Include(a => a.EmteaType)
                    .ThenInclude(a => a.EmteaGroup)
                    .ThenInclude(a=>a.Emtea)
                    .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                var response = new TuikSubeDto
                {
                   Id =model.Id,
                   AddedUser = model.AddUser.Name + model.AddUser.Surname,
                   AddSicil = model.AddUser.SicilNumber,
                   AddedTime = model.AddedTime,
                   EmteaCode = model.EmteaType.EmteaGroup.Emtea.EmteaCode,
                   EmteaName = model.EmteaType.EmteaGroup.Emtea.EmteaName,
                   EmteaGroupName = model.EmteaType.EmteaGroup.GroupName,
                   EmteaTypeName = model.EmteaType.EmteaTypeName,
                   GuessValue = model.GuessValue,
                   GuessYear = model.GuessYear,
                   IsCity = model.IsCity,
                   TuikValue = model.TuikValue,
                   TuikYear = model.TuikYear,
                   //UpdatedUser = model.UpdateUser.Name + model.UpdateUser.Surname,
                   UpdatedTime = model.UpdatedTime,
                   //UpdateSicil = model.UpdateUser.SicilNumber,
                   SubeName = model.Sube.SubeName,
                   //CityName = model.City.Name
                };

                return new NIslemSonuc<TuikSubeDto>
                {
                    BasariliMi = true,
                    Veri = response
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<TuikSubeDto>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
    }
}
