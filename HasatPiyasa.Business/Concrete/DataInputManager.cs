﻿using DevExpress.Data.ExpressionEditor;
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
using System.Transactions;

namespace HasatPiyasa.Business.Concrete
{
    public class DataInputManager : IDataInputService
    {
        private IDataInputDal _dataInputDal;
        private IFormDataInputDal _formDataInputDal;
        private ICityDal _cityDal;
        private ISubeDal _subeDal;
        private IBolgeDal _bolgeDal;

        public DataInputManager(IDataInputDal dataInputDal, IFormDataInputDal formDataInputDal, ICityDal cityDal, ISubeDal subeDal, IBolgeDal bolgeDal)
        {
            _dataInputDal = dataInputDal;
            _formDataInputDal = formDataInputDal;
            _cityDal = cityDal;
            _subeDal = subeDal;
            _bolgeDal = bolgeDal;
        }

        public async Task<NIslemSonuc<DataInputs>> CreateDataInput(DataInputs dataInput)
        {
            try
            {
                var addeddatainput = await _dataInputDal.AddAsync(dataInput);

                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = true,
                    Veri = addeddatainput,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<DataInputs>> CreateDataInputRange(List<DataInputs> dataInputs, int cityid, int subeid, int userid)
        {
            try
            {
                var dataInputCheckId = dataInputs.Where(x => x.Id > 0).FirstOrDefault();


                if (dataInputCheckId != null)
                {
                    var DataInputGetAddTime = _dataInputDal.GetTable().Result.FirstOrDefault(x => x.Id == dataInputCheckId.Id);

                    if (DataInputGetAddTime.AddedTime.Date == DateTime.Now.Date)
                    {
                        var formId = 0;
                        dataInputs.ForEach(f =>
                        {
                            if (f.Id > 0)
                            {
                                formId = _dataInputDal.Get(x => x.Id == f.Id).FormDataInputId;

                                using (var dbcontext = new HasatPiyasaContext())
                                {
                                    var update = dbcontext.Entry(f);
                                    update.Entity.FormDataInputId = formId;
                                    update.Entity.UpdatedTime = DateTime.Now;
                                    update.Entity.UpdateUserId = userid;
                                    update.Entity.AddUserId = DataInputGetAddTime.AddUserId;
                                    update.Entity.AddedTime = DataInputGetAddTime.AddedTime;
                                    update.State = EntityState.Modified;
                                    var count = dbcontext.SaveChanges();
                                }

                            }
                            else
                            {
                                f.FormDataInputId = formId;
                                f.AddUserId = userid;
                                f.AddedTime = DateTime.Now;
                                var addedDataInputItem = _dataInputDal.Add(f);
                            }

                        });

                        return new NIslemSonuc<DataInputs>
                        {
                            BasariliMi = true,
                            Mesaj = Messages.DataInputUpdate
                        };
                    }
                    else
                    {
                        await AddNewRecord(dataInputs, cityid, subeid, userid);
                        return new NIslemSonuc<DataInputs>
                        {
                            BasariliMi = true,
                            Mesaj = Messages.DataInputAdd
                        };
                    }



                }
                else
                {
                    await AddNewRecord(dataInputs, cityid, subeid, userid);

                    return new NIslemSonuc<DataInputs>
                    {
                        BasariliMi = true,
                        Mesaj = Messages.DataInputAdd
                    };
                }

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };

            }
        }

        private async Task AddNewRecord(List<DataInputs> dataInputs, int cityid, int subeid, int userid)
        {
            FormDataInput formDataInput = new FormDataInput
            {
                AddedTime = DateTime.Now,
                IsActive = true,
                IsLock = false,
                CityId = cityid,
                SubeId = subeid,
                EmteaId = dataInputs.FirstOrDefault().EmteaId
            };

            var addedformdt = await _formDataInputDal.AddAsync(formDataInput);

            dataInputs.ForEach(x =>
            {
                x.FormDataInputId = formDataInput.Id;
                x.Id = 0;
                x.AddedTime = DateTime.Now;
                x.AddUserId = userid;
            });
            await _dataInputDal.AddRange(dataInputs);
        }

        private NIslemSonuc<bool> CheckDataInputForm(int cityId)
        {
            if (_formDataInputDal.Get(p => p.CityId == cityId && p.AddedTime == DateTime.Today) != null)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.DataInputControlError
                };
            }
            return new NIslemSonuc<bool>
            {
                BasariliMi = true
            };
        }

        public NIslemSonuc<DataInputs> GetDataInput(int id)
        {
            try
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = true,
                    Veri = _dataInputDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<DataInputs>> GetDataInputTable(int value)
        {
            try
            {
                var res = await _dataInputDal.GetTable();

                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.City).Include(x => x.Sube).Include(x => x.EmteaType).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }



        public NIslemSonuc<List<DataInputs>> ListAllDataInputs()
        {
            try
            {
                return new NIslemSonuc<List<DataInputs>>
                {
                    BasariliMi = true,
                    Veri = _dataInputDal.GetList(x => x.IsActive == true).ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<DataInputs>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<ReportDto>> ListDataInputsForCityMarket(string[] dates, string[] emteatypes)
        {
            List<ReportDto> res = new List<ReportDto>();
            List<DataInputs> resp = new List<DataInputs>();
            var cities = _cityDal.GetList().ToList();
            for (int j = 0; j < emteatypes.Length; j++)
                for (int i = 0; i < dates.Length; i++)
                {
                    var all = _dataInputDal.GetList(x => x.AddedTime.Date == Convert.ToDateTime(dates[i]) && x.EmteaTypeId == Convert.ToInt32(emteatypes[j]) && x.IsActive == true).ToList();
                    resp.AddRange(all);
                }
            var response = resp.Select(a => new ReportDto
            {

                CityId = a.CityId,
                CityAdi = cities.Where(x => x.Id == a.CityId).First().Name,
                TuikValue = resp.Where(x => x.CityId == a.CityId).Sum(x => x.TuikValue),
                GuessValue = resp.Where(x => x.CityId == a.CityId).Sum(x => x.GuessValue),
                HasatMiktar = resp.Where(x => x.CityId == a.CityId).Sum(x => x.HasatMiktar),
                Natural1 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural1),
                Natural2 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural2),
                Natural3 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural3),
                Natural4 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural4),
                Natural5 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural5),
                NaturalToplam = resp.Where(x => x.CityId == a.CityId).Sum(x => x.NaturalToplam),
                ToptanPiyasa1 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.ToptanPiyasa1 * x.Natural1) / resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural1),
                ToptanPiyasa2 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.ToptanPiyasa2 * x.Natural2) / resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural2),
                ToptanPiyasa3 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.ToptanPiyasa3 * x.Natural3) / resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural3),
                ToptanPiyasa4 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.ToptanPiyasa4 * x.Natural4) / resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural4),
                ToptanPiyasa5 = resp.Where(x => x.CityId == a.CityId).Sum(x => x.ToptanPiyasa5 * x.Natural5) / resp.Where(x => x.CityId == a.CityId).Sum(x => x.Natural5),
                Perakende1 = resp.Where(x => x.CityId == a.CityId & x.Perakende1 > 0).Average(x => x.Perakende1),
                Perakende2 = resp.Where(x => x.CityId == a.CityId & x.Perakende2 > 0).Average(x => x.Perakende2),
                Perakende3 = resp.Where(x => x.CityId == a.CityId & x.Perakende3 > 0).Average(x => x.Perakende3),
                Perakende4 = resp.Where(x => x.CityId == a.CityId & x.Perakende4 > 0).Average(x => x.Perakende4),
                Perakende5 = resp.Where(x => x.CityId == a.CityId & x.Perakende5 > 0).Average(x => x.Perakende5),
                Perakende6 = resp.Where(x => x.CityId == a.CityId & x.Perakende6 > 0).Average(x => x.Perakende6),


                //HasatOran = res.Where(x => x.CityId == a.CityId).Sum(x => x.TuikValue),
                //HasatMiktar = res.Where(x => x.CityId == a.CityId).Sum(x => x.HasatMiktar),




            }).ToList();
            //var resp = await _dataInputDal.GetWhere(x => x.AddedTime.ToShortDateString() == dates[i] && emtealar.Contains(x.EmteaTypeId.ToString()));
            // var res = _dataInputDal.GetList().ToList();
            // var resp = res.Where(x => dates.Contains(x.AddedTime.ToShortDateString()) && emteatypes.Contains(x.EmteaTypeId.ToString())).ToList();


            try
            {
                return new NIslemSonuc<List<ReportDto>>
                {
                    BasariliMi = true,
                    //Veri = _dataInputDal.GetList().Where(x => dates.Contains(x.AddedTime.ToShortDateString())).ToList()
                    //Veri = _dataInputDal.GetList(x => dates.Contains(x.AddedTime.ToShortDateString()) && emteatypes.Contains(x.EmteaTypeId.ToString())).ToList()
                    Veri = response.GroupBy(c => c.CityId).Select(g => g.First()).ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<ReportDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }


        public NIslemSonuc<List<ReportDto>> ListDataInputsForSubeMarket(string[] dates, string[] emteatypes)
        {
            List<ReportDto> res = new List<ReportDto>();
            List<DataInputs> resp = new List<DataInputs>();
            var subeler = _subeDal.GetList().ToList();
            var bolgeler = _bolgeDal.GetList().ToList();
            for (int j = 0; j < emteatypes.Length; j++)
                for (int i = 0; i < dates.Length; i++)
                {
                    var all = _dataInputDal.GetList(x => x.AddedTime.Date == Convert.ToDateTime(dates[i]) && x.EmteaTypeId == Convert.ToInt32(emteatypes[j]) & x.IsActive == true).ToList();
                    resp.AddRange(all);
                }
            var response = resp.Select(a => new ReportDto
            {

                SubeId = a.SubeId,
                SubeAdi = subeler.Where(x => x.Id == a.SubeId).First().SubeName,
                BolgeAdi = bolgeler.Where(x => x.Id == subeler.Where(x => x.Id == a.SubeId).First().BolgeId).First().Name,
                TuikValue = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.TuikValue),
                GuessValue = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.GuessValue),
                HasatMiktar = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.HasatMiktar),
                HasatOran = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.HasatOran * x.GuessValue) / resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.GuessValue),
                Natural1 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural1),
                Natural2 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural2),
                Natural3 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural3),
                Natural4 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural4),
                Natural5 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural5),
                NaturalToplam = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.NaturalToplam),
                ToptanPiyasa1 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.ToptanPiyasa1 * x.Natural1) / resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural1),
                ToptanPiyasa2 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.ToptanPiyasa2 * x.Natural2) / resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural2),
                ToptanPiyasa3 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.ToptanPiyasa3 * x.Natural3) / resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural3),
                ToptanPiyasa4 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.ToptanPiyasa4 * x.Natural4) / resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural4),
                ToptanPiyasa5 = resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.ToptanPiyasa5 * x.Natural5) / resp.Where(x => x.SubeId == a.SubeId).Sum(x => x.Natural5),
                Perakende1 = resp.Where(x => x.SubeId == a.SubeId & x.Perakende1 > 0).Average(x => x.Perakende1),
                Perakende2 = resp.Where(x => x.SubeId == a.SubeId & x.Perakende2 > 0).Average(x => x.Perakende2),
                Perakende3 = resp.Where(x => x.SubeId == a.SubeId & x.Perakende3 > 0).Average(x => x.Perakende3),
                Perakende4 = resp.Where(x => x.SubeId == a.SubeId & x.Perakende4 > 0).Average(x => x.Perakende4),
                Perakende5 = resp.Where(x => x.SubeId == a.SubeId & x.Perakende5 > 0).Average(x => x.Perakende5),
                Perakende6 = resp.Where(x => x.SubeId == a.SubeId & x.Perakende6 > 0).Average(x => x.Perakende6),


                //HasatOran = res.Where(x => x.CityId == a.CityId).Sum(x => x.TuikValue),
                //HasatMiktar = res.Where(x => x.CityId == a.CityId).Sum(x => x.HasatMiktar),




            }).ToList();
            //var resp = await _dataInputDal.GetWhere(x => x.AddedTime.ToShortDateString() == dates[i] && emtealar.Contains(x.EmteaTypeId.ToString()));
            // var res = _dataInputDal.GetList().ToList();
            // var resp = res.Where(x => dates.Contains(x.AddedTime.ToShortDateString()) && emteatypes.Contains(x.EmteaTypeId.ToString())).ToList();


            try
            {
                return new NIslemSonuc<List<ReportDto>>
                {
                    BasariliMi = true,
                    //Veri = _dataInputDal.GetList().Where(x => dates.Contains(x.AddedTime.ToShortDateString())).ToList()
                    //Veri = _dataInputDal.GetList(x => dates.Contains(x.AddedTime.ToShortDateString()) && emteatypes.Contains(x.EmteaTypeId.ToString())).ToList()
                    Veri = response.GroupBy(c => c.SubeId).Select(g => g.First()).OrderBy(b => b.BolgeAdi).ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<ReportDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<DataInputs>> UpdateDataInput(DataInputs dataInput)
        {
            try
            {
                var updateddatainput = await _dataInputDal.UpdateAsync(dataInput);

                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = true,
                    Veri = updateddatainput,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

      
        public async Task<NIslemSonuc<List<DataInputTbaleListModelForRiceDto>>> GetDataInputsTableForm(int id)
        {
            try
            {
                var res = await _dataInputDal.GetTable();
                var model = res.AsQueryable().Include(x => x.City).Include(x=>x.AddUser).Include(x=>x.UpdateUser).Include(x => x.Sube).Include(x => x.EmteaType).ThenInclude(x=>x.EmteaGroup).ThenInclude(x=>x.Emtea).Where(x => x.FormDataInputId == id).ToList();

                var response = model.Select(x => new DataInputTbaleListModelForRiceDto
                {
                    AddUser = x.AddUser.Name+ " " + x.AddUser.Surname,
                    AlimYear = x.AlimYear,
                    Cityname = x.City.Name,
                    EmteaGroupName = x.EmteaType.EmteaGroup.GroupName,
                    EmteaName = x.EmteaType.EmteaGroup.Emtea.EmteaName,
                    EmteaTypeName = x.EmteaType.EmteaTypeName,
                    FormDataInputId = x.FormDataInputId,
                    GuessValue = x.GuessValue,
                    HasatMiktar = x.HasatMiktar,
                    HasatOran = x.HasatOran,
                    Id = x.Id,
                    Natural1 = x.Natural1,
                    Natural2 = x.Natural2,
                    Natural3 = x.Natural3,
                    Natural4 = x.Natural4,
                    Natural5 = x.Natural5,
                    NaturalToplam = x.NaturalToplam,
                    Perakende1 = x.Perakende1,
                    Perakende2 = x.Perakende2,
                    Perakende3 = x.Perakende3,
                    Perakende4 = x.Perakende4,
                    Perakende5 = x.Perakende5,
                    Perakende6 = x.Perakende6,
                    PerakendeToplam = x.PerakendeToplam,
                    SubeName = x.Sube.SubeName,
                    ToptanPiyasa1 = x.ToptanPiyasa1,
                    ToptanPiyasa2 = x.ToptanPiyasa2,
                    ToptanPiyasa3 = x.ToptanPiyasa3,
                    ToptanPiyasa4 = x.ToptanPiyasa4,
                    ToptanPiyasa5 = x.ToptanPiyasa5,
                    ToptanPiyasaToplam = x.ToptanPiyasaToplam,
                    TuikValue = x.TuikValue,
                    //UpdateUser = x.UpdateUser.Name + " " + x.UpdateUser.Surname,
                    UreticiKalanMiktar = x.UreticiKalanMiktar
                }).OrderBy(u=>u.Id).ToList();

                

                return new NIslemSonuc<List<DataInputTbaleListModelForRiceDto>>
                {
                    BasariliMi = true,
                    Veri = response

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<DataInputTbaleListModelForRiceDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
    }
}
