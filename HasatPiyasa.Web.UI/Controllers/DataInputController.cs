using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo.Metadata.Helpers;
using HasatPiyasa.Business;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.FilterAttributes;
using HasatPiyasa_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class DataInputController : BaseController
    {
        private IDataInputService _dataInputService;
        private IEmteaService _emteaService;
        private ICityService _cityService;
        private ISubeCityService _subeCityService;
        private readonly IFormDataInputService _formDataInputService;

        public DataInputController(IDataInputService dataInputService, IEmteaService emteaService ,ICityService cityService, ISubeCityService subeCityService ,IFormDataInputService formDataInputService)
        {
            _dataInputService = dataInputService;
            _emteaService = emteaService;
            _cityService = cityService;
            _subeCityService = subeCityService;
            _formDataInputService = formDataInputService;
        }
      
        [HttpGet]      
        public async Task<ActionResult> DataInputRice(int cityId=0)
        { 
            

            
            var model = new HasaInputViewModel();
            int em = (int)Core.Utilities.Enums.DataInput.Data.Rice;
            var emtea = await _emteaService.GetEmteaTable(em,cityId);
            var user = GetCurrentUser();
            if(user!=null)
            {
                var cities = await _subeCityService.GetSubeCityGTable(user.SubeId);
                model.Cities = cities.Veri.Where(x=>x.SubeId ==user.SubeId).OrderBy(x=>x.Id).ToList();
            }            
            model.Emteas = emtea.Veri;

            if (cityId == 0)
            {
                
                cityId = GetCurrentUser().Sube.SubeCities.FirstOrDefault().CityId;
                model.SelectedCityId = cityId;
                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId);
                model.DataInputs = Inputs.Veri!=null ? _dataInputService.ListAllDataInputs().Veri.Where(x=>x.FormDataInputId==Inputs.Veri.Id).ToList():null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;

                if(Inputs.Veri.AddedTime!=DateTime.Today)
                {
                    model.DataInputs.ForEach(x =>
                    {
                        x.Id = 0;
                    });
                    
                }
            }
            else
            {
                model.SelectedCityId = cityId;
               var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId);
                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;

                if (Inputs.Veri.AddedTime != DateTime.Today)
                {
                    model.DataInputs.ForEach(x =>
                    {
                        x.Id = 0;
                    });

                }
            }            

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DataInputRice(List<DataInputs> dataInputs)
        {
            var user = GetCurrentUser();

            dataInputs.ForEach(x =>
            {
                x.SubeId = user.SubeId;
                x.AddUserId = user.UserId;
                x.AlimYear = DateTime.Now.Year;
                x.EmteaId = (int)Core.Utilities.Enums.DataInput.Data.Rice;
                x.AddedTime = DateTime.Today;
            });

            if(dataInputs.Count>0)
            {
                var cityId = dataInputs.FirstOrDefault().CityId;
                var response = await _dataInputService.CreateDataInputRange(dataInputs, cityId, user.SubeId);
                if (response.BasariliMi)
                {
                    return Json(new { success = true, messages = response.Mesaj });
                }
                else
                {
                    return Json(new { success = false, messages = response.Mesaj });
                }
            }
            else
            {
                return Json(new { success = false, messages = "Lütfen formu doldudurun !" });
            }
           

        }

        [HttpGet]//Hububat Get
        public async Task<ActionResult> DataImputCereal()
        {
            return View();
        }

        [HttpPost] //Hububat Post
        public async Task<ActionResult> DataInputCereal(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Mısır Get
        public async Task<ActionResult> DataImputCorn(int cityId = 0)
        {
            return View();
        }

        [HttpPost] //Mısır Post
        public async Task<ActionResult> DataInputCorn(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Kırmızı Mercimek Get
        public async Task<ActionResult> DataImputRedLentil(int cityId = 0)
        {
            return View();
        }

        [HttpPost] //Kırmızı Mercimek Post
        public async Task<ActionResult> DataInputRedLentil(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Yeşil Mercimek Get
        public async Task<ActionResult> DataImputGreenLentil(int cityId = 0)
        {
            return View();
        }

        [HttpPost] //Yeşil Mercimek Post
        public async Task<ActionResult> DataInputGreenLentil(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Kuru Fasulye Get
        public async Task<ActionResult> DataImputBean(int cityId = 0)
        {
            return View();
        }

        [HttpPost] //Kuru Fasulye Post
        public async Task<ActionResult> DataInputBean(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Nohut Get
        public async Task<ActionResult> DataImputChickPea(int cityId = 0)
        {
            return View();
        }

        [HttpPost] //Nohut Post
        public async Task<ActionResult> DataInputChickPea(List<DataInputs> dataInputs)
        {
            return View();
        }


    }
}