using DevExpress.Data.ExpressionEditor;
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

        public DataInputManager(IDataInputDal dataInputDal, IFormDataInputDal formDataInputDal, ICityDal cityDal, ISubeDal subeDal)
        {
            _dataInputDal = dataInputDal;
            _formDataInputDal = formDataInputDal;
            _cityDal = cityDal;
            _subeDal = subeDal;
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

        public async Task<NIslemSonuc<DataInputs>> CreateDataInputRange(List<DataInputs> dataInputs, int cityid, int subeid)
        {
            try
            {
                if (dataInputs.Where(x => x.Id == 0).Count() != dataInputs.Count() && dataInputs.FirstOrDefault().AddedTime.Date == DateTime.Now.Date )
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
                                update.State = EntityState.Modified;
                                var count = dbcontext.SaveChanges();
                            }

                        }
                        else
                        {
                            f.FormDataInputId = formId;
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

                    dataInputs.ForEach(x => x.FormDataInputId = formDataInput.Id);
                    dataInputs.ForEach(x => x.Id = 0);

                    await _dataInputDal.AddRange(dataInputs);

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
                    Veri = _dataInputDal.GetList().ToList()
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
                    var all = _dataInputDal.GetList(x => x.AddedTime.Date == Convert.ToDateTime(dates[i]) && x.EmteaTypeId == Convert.ToInt32(emteatypes[j])).ToList();
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
                Perakende1 = resp.Where(x => x.CityId == a.CityId).Average(x => x.Perakende1),
                Perakende2 = resp.Where(x => x.CityId == a.CityId).Average(x => x.Perakende2),
                Perakende3 = resp.Where(x => x.CityId == a.CityId).Average(x => x.Perakende3),
                Perakende4 = resp.Where(x => x.CityId == a.CityId).Average(x => x.Perakende4),
                Perakende5 = resp.Where(x => x.CityId == a.CityId).Average(x => x.Perakende5),
                Perakende6 = resp.Where(x => x.CityId == a.CityId).Average(x => x.Perakende6),


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
            for (int j = 0; j < emteatypes.Length; j++)
                for (int i = 0; i < dates.Length; i++)
                {
                    var all = _dataInputDal.GetList(x => x.AddedTime.Date == Convert.ToDateTime(dates[i]) && x.EmteaTypeId == Convert.ToInt32(emteatypes[j])).ToList();
                    resp.AddRange(all);
                }
            var response = resp.Select(a => new ReportDto
            {

                SubeId = a.SubeId,
                SubeAdi = subeler.Where(x => x.Id == a.SubeId).First().SubeName,
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
                Perakende1 = resp.Where(x => x.SubeId == a.SubeId).Average(x => x.Perakende1),
                Perakende2 = resp.Where(x => x.SubeId == a.SubeId).Average(x => x.Perakende2),
                Perakende3 = resp.Where(x => x.SubeId == a.SubeId).Average(x => x.Perakende3),
                Perakende4 = resp.Where(x => x.SubeId == a.SubeId).Average(x => x.Perakende4),
                Perakende5 = resp.Where(x => x.SubeId == a.SubeId).Average(x => x.Perakende5),
                Perakende6 = resp.Where(x => x.SubeId == a.SubeId).Average(x => x.Perakende6),


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
                    Veri = response.GroupBy(c => c.SubeId).Select(g => g.First()).ToList()
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

        public Task<NIslemSonuc<List<DataInputDto>>> GetDataInputGTable()
        {
            throw new NotImplementedException();
        }

        
    }
}
