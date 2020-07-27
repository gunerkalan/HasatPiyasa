﻿using HasastPiyasa.DataAccess.Abstract;
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
    public class FormDataInputManager : IFormDataInputService
    {
        private IFormDataInputDal _formDataInputDal;

        public FormDataInputManager(IFormDataInputDal formDataInputDal)
        {
            _formDataInputDal = formDataInputDal;
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

        public async Task<NIslemSonuc<List<FormDataInput>>> GetFormDataGTable()
        {
            try
            {
                var res = await _formDataInputDal.GetTable();
                var response = res.Include(x => x.City).Include(x => x.Sube).Where(x => x.IsActive).ToList();



                return new NIslemSonuc<List<FormDataInput>>
                {
                    BasariliMi = false,
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

                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = true,
                    Veri = res.AsNoTracking().Include(x => x.DataInputs).ThenInclude(x => x.EmteaType).ThenInclude(x => x.EmteaGroup).ThenInclude(x => x.Emtea).
                    Where(x => x.AddedTime == date && x.IsActive && x.IsLock == false && x.CityId == cityId).FirstOrDefault()
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

                    };
                }
                else
                {
                    return new NIslemSonuc<FormDataInput>
                    {
                        BasariliMi = false

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
