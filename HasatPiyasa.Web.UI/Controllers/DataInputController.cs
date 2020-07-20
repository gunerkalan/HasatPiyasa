using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HasatPiyasa.Business;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Entity;
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
            var emtea = await _emteaService.GetEmteaTable(1,cityId);
            var user = GetCurrentUser();
            if(user!=null)
            {
                var cities = await _subeCityService.GetSubeCityGTable(user.SubeId);
                model.Cities = cities.Veri.Where(x=>x.SubeId ==user.SubeId).ToList();
            }            
            model.Emteas = emtea.Veri;

            if (cityId == 0)
            {
                cityId = GetCurrentUser().Sube.SubeCities.FirstOrDefault().Id;
                model.SelectedCityId = cityId;
                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId);
                model.DataInputs = Inputs.Veri!=null ? Inputs.Veri.DataInputs.ToList():null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;
            }
            else
            {
                model.SelectedCityId = cityId;
               var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId);
                model.DataInputs = Inputs.Veri != null ? Inputs.Veri.DataInputs.ToList() : null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;
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
                x.EmteaId = 1;
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

      

    }
}