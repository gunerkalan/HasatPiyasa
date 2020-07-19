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
        ICityService _cityService;
        public DataInputController(IDataInputService dataInputService, IEmteaService emteaService ,ICityService cityService)
        {
            _dataInputService = dataInputService;
            _emteaService = emteaService;
            _cityService = cityService;
        }
      
        [HttpGet]
        public async Task<ActionResult> DataInputRice(int cityId=0)
        {
            var userCity = ControllerContext.HttpContext.Session.Get<Users>("User");
           


            var model = new HasaInputViewModel();
            var emtea = await _emteaService.GetEmteaTable(1,cityId);
            var user = GetCurrentUser();
            if(user!=null)
            {
                var cities=  _cityService.ListAllCities().Veri;
                model.Cities = cities.Where(x=>x.SubeId==user.SubeId).ToList();
            }            
            model.Emteas = emtea.Veri;

            if (cityId == 0)
            {
                cityId = userCity.Sube.Cities.FirstOrDefault().Id;
                model.SelectedCityId = cityId;
            }
            else
            {
                model.SelectedCityId = cityId;
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
            });

            var response = await _dataInputService.CreateDataInputRange(dataInputs);
            if(response.BasariliMi)
            {
                return Json(new { success = true , messages = response.Mesaj });
            }
            else
            {
                return Json(new { success = false, messages = response.ErrorMessage });
            }
        }

    }
}