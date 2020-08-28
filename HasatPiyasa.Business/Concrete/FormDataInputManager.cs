using HasastPiyasa.DataAccess.Abstract;
using HasastPiyasa.DataAccess.Concrete;
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
    
    public class FormDataInputManager : EfFormDataInputDal,IFormDataInputService
    {
        private IFormDataInputDal _formDataInputDal;
        private IEmteaService _emteaservice;

        public FormDataInputManager(IFormDataInputDal formDataInputDal, IEmteaService emteaService)
        {
            _formDataInputDal = formDataInputDal;
            _emteaservice = emteaService;
        }

        public async Task<NIslemSonuc<FormDataInput>> CreateFormDataInput(FormDataInput formDataInput)
        {
            try
            {

                var addedform = await _formDataInputDal.AddAsync(formDataInput);

                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = true,
                    Veri = addedform
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<FormDataInputDto>>> GetFormDataInputGTable(int? SubeId, int EmteaId)
        {
            try
            {
                var res = await _formDataInputDal.GetTable();

                var resc = _emteaservice.GetEmteaAsync(EmteaId);

                if (SubeId!=null)
                {
                    var model = res.Include(x => x.Sube).Include(x => x.City).Where(x => x.IsActive && x.SubeId == SubeId && x.EmteaId == EmteaId).AsNoTracking().ToList();

                    var response = model.Select(x => new FormDataInputDto
                    {
                        Id = x.Id,
                        AddedTime = x.AddedTime,
                        CityId = x.CityId,
                        CityName = x.City.Name,
                        EmteaId = x.EmteaId,
                        IsActive = x.IsActive,
                        IsLock = x.IsLock,
                        SubeId = x.SubeId,
                        SubeName = x.Sube.SubeName,
                        SubeCode = x.Sube.SubeKod,
                        UpdatedTime = x.UpdatedTime,

                    }).OrderByDescending(u => u.Id).ToList();

                    response.ForEach(x =>
                    {
                        x.EmteaName = resc.Result.Veri.EmteaName;
                        x.EmteaCode = resc.Result.Veri.EmteaCode;
                    });


                    return new NIslemSonuc<List<FormDataInputDto>>
                    {
                        BasariliMi = false,
                        Veri = response
                    };
                }
                else
                {
                    var model = res.Include(x => x.Sube).Include(x => x.City).Where(x => x.IsActive).AsNoTracking().ToList();

                    var response = model.Select(x => new FormDataInputDto
                    {
                        Id = x.Id,
                        AddedTime = x.AddedTime,
                        CityId = x.CityId,
                        CityName = x.City.Name,
                        EmteaId = x.EmteaId,
                        IsActive = x.IsActive,
                        IsLock = x.IsLock,
                        SubeId = x.SubeId,
                        SubeName = x.Sube.SubeName,
                        SubeCode = x.Sube.SubeKod,
                        UpdatedTime = x.UpdatedTime,

                    }).OrderByDescending(u=>u.Id).ToList();

                    response.ForEach(x =>
                    {
                        x.EmteaName = resc.Result.Veri.EmteaName;
                        x.EmteaCode = resc.Result.Veri.EmteaCode;
                    });


                    return new NIslemSonuc<List<FormDataInputDto>>
                    {
                        BasariliMi = false,
                        Veri = response
                    };
                }

               

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<FormDataInputDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<FormDataInput>>> GetFormDataGTable()
        {
            try
            {
                var res = await _formDataInputDal.GetTable();
                var response = res.Include(x => x.City).Include(x => x.Sube).Where(x => x.IsActive).ToList();



                return new NIslemSonuc<List<FormDataInput>>
                {
                    BasariliMi = true,
                    Veri = response
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<FormDataInput>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<FormDataInput>> GetFormDataInputTable(DateTime date, int cityId)
        {
            try
            {
                var res = await _formDataInputDal.GetTable();
                var result = res.AsNoTracking().Include(x => x.DataInputs).ThenInclude(x => x.EmteaType).ThenInclude(x => x.EmteaGroup).ThenInclude(x => x.Emtea).
                    Where(x => x.IsActive && x.CityId == cityId).OrderByDescending(x => x.Id).FirstOrDefault();


                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = true,
                    Veri = result
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<DateTime>>> GetReporFormDataGTable(int id)
        {
            try
            {
                var res = await _formDataInputDal.GetTable();
                var model = res.Where(x => x.IsActive && x.EmteaId==id).Select(x=>x.AddedTime).Distinct().ToList();

                //var response = model.Select(x => new FormDataReportDto
                //{
                //    Date = x.AddedTime.ToShortDateString()
                //}).ToList();


                return new NIslemSonuc<List<DateTime>>
                {
                    BasariliMi = true,
                    Veri = model
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<DateTime>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<FormDataInput>> UpdateFormData(int emteaid, int subeid, int cityid, bool state, int formid)
        {
            try
            {
                var update = _formDataInputDal.Get(u => u.EmteaId == emteaid && u.SubeId == subeid && u.CityId == cityid && u.Id == formid);

                update.IsLock = state;

                var updatedemteatype = await _formDataInputDal.UpdateAsync(update);

                if (updatedemteatype != null)
                {
                    return new NIslemSonuc<FormDataInput>
                    {
                        BasariliMi = true,
                        Veri = updatedemteatype,
                        Mesaj = Messages.IsLockSuccess

                    };
                }
                else
                {
                    return new NIslemSonuc<FormDataInput>
                    {
                        BasariliMi = false,
                        Mesaj = Messages.IsLockSuccess
                    };
                }
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}
