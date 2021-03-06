﻿using DevExpress.Utils.Filtering.Internal;
using DevExpress.Utils.Serializing;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class SubeManager : ISubeService
    {
        private ISubeDal _subeDal;

        public SubeManager(ISubeDal subeDal)
        {
            _subeDal = subeDal;
        }

        public async Task<NIslemSonuc<Subes>> CreateSube(Subes sube)
        {
            try
            {
                var addedsube = await _subeDal.AddAsync(sube);

                return new NIslemSonuc<Subes>
                {
                    BasariliMi = true,
                    Veri = addedsube,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<SubeDto>>> GetSubeGTable()
        {
            try
            {
                var res = await _subeDal.GetTable();
                var model = res.Include(x => x.Bolge).Include(x => x.SubeCities).ThenInclude(x => x.City).Where(x => x.IsActive).ToList();

                var response = model.Select(x => new SubeDto
                {
                    BolgeName = x.Bolge.Name,
                    SubeCode = x.SubeKod,
                    SubeName = x.SubeName,
                    Id = x.Id,
                    AddedDate = x.AddedTime,
                    Cities = string.Join(',', x.SubeCities.Select(x => x.City.Name).ToArray())

                }).ToList();

                return new NIslemSonuc<List<SubeDto>>
                {
                    BasariliMi = false,
                    Veri = response
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<SubeDto>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<Subes> GetSube(int id)
        {
            try
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = true,
                    Veri = _subeDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
        public NIslemSonuc<List<Subes>> GetSubesByBolges(string[] bolges)
        {

            try
            {
                var res = _subeDal.GetList().ToList();
                return new NIslemSonuc<List<Subes>>
                {
                    BasariliMi = true,
                    Veri = res.Where(x => bolges.Contains(x.BolgeId.ToString())).ToList()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<Subes>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
        public async Task<NIslemSonuc<Subes>> GetSubeTable(int value)
        {
            try
            {
                var res = await _subeDal.GetTable();

                return new NIslemSonuc<Subes>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.Tuiks).Include(x => x.Bolge).Include(x => x.SubeCities).ThenInclude(x => x.City).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Subes>> ListAllSubes()
        {
            try
            {
                return new NIslemSonuc<List<Subes>>
                {
                    BasariliMi = true,
                    Veri = _subeDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Subes>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Subes>> UpdateSube(Subes sube)
        {
            try
            {
                var updatedsube = await _subeDal.UpdateAsync(sube);

                return new NIslemSonuc<Subes>
                {
                    BasariliMi = true,
                    Veri = updatedsube,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Subes>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<SubeCityDto>>> GetSbGTable()
        {
            try
            {
                var res = await _subeDal.GetTable();
                var model = res.Include(x => x.SubeCities).ThenInclude(x => x.City).Where(x => x.IsActive).ToList();

                var response = model.Select(x => new SubeCityDto
                {
                    SubeId = x.Id,
                    SubeName = x.SubeName
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

        public async Task<NIslemSonuc<List<SubeFormDataWDataInput>>> GetSubeGTableWithFormDatas(int emteaid)
        {
            try
            {
                var res = await _subeDal.GetTable();
                var model = res.Include(x => x.Bolge).Include(x => x.SubeCities).ThenInclude(x => x.City).Include(x => x.FormDataInputs).Where(x => x.IsActive).OrderBy(x=>x.Bolge.Name).ToList();

                var modelhavedata = model.Where(x => x.FormDataInputs.Where(x=>x.AddedTime.Date == DateTime.Now.Date).Count()>0).ToList();

                bool status=false;

                var models = new List<SubeFormDataWDataInput>();
                modelhavedata.ForEach(x =>
                {

                    if (x.FormDataInputs.Where(y => y.IsActive && y.EmteaId == emteaid).Count() > 0)
                    {
                        var citiesIds = x.SubeCities.Select(s => s.CityId).ToArray();
                        var _haveDataCities = x.FormDataInputs.Where(x => citiesIds.Contains(x.CityId) && x.AddedTime.Date ==DateTime.Now.Date).Select(s => s.CityId).Distinct().ToList();

                        if(x.SubeCities.Count() == _haveDataCities.Count())
                        {
                            status = true;
                        }
                        else
                        {
                            status= false;
                        }
                        
                        var response = new SubeFormDataWDataInput
                        {
                            BolgeName = x.Bolge.Name,
                            SubeCode = x.SubeKod,
                            SubeName = x.SubeName,
                            Id = x.Id,
                            AddedDate = string.Join(',', x.AddedTime.ToShortTimeString().ToArray()),
                            Cities = string.Join(',', x.SubeCities.Select(x => x.City.Name).ToArray()),
                            IsHavaData = status,
                            IsHaveDataCount = _haveDataCities.Count(),
                            HaveDataCities = string.Join(',', x.SubeCities.Where(c => _haveDataCities.Contains(c.CityId)).Select(s => s.City.Name).ToArray())
                        };

                        models.Add(response);

                    }
                   

                });



                var modelnohavedata = model.Where(x => x.FormDataInputs.Where(x => x.AddedTime.Date == DateTime.Now.Date).Count() == 0).ToList();

                modelnohavedata.ForEach(x =>
                {

                    if (x.FormDataInputs.Where(y => y.IsActive && y.EmteaId == emteaid).Count() > 0)
                    {
                        var citiesIds = x.SubeCities.Select(s => s.CityId).ToArray();
                       
                        var response = new SubeFormDataWDataInput
                        {
                            BolgeName = x.Bolge.Name,
                            SubeCode = x.SubeKod,
                            SubeName = x.SubeName,
                            Id = x.Id,
                            AddedDate = string.Join(',', x.AddedTime.ToShortTimeString().ToArray()),
                            Cities = string.Join(',', x.SubeCities.Select(x => x.City.Name).ToArray()),
                            IsHavaData = false,
                            IsHaveDataCount =0,
                            HaveDataCities = "Veri Girişi Yok"
                        };

                        models.Add(response);

                    }


                });


                return new NIslemSonuc<List<SubeFormDataWDataInput>>
                {
                    BasariliMi = false,
                    Veri = models
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<SubeFormDataWDataInput>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
    }
}
