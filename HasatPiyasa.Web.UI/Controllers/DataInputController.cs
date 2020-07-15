using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> DataInputRice()
        {
            var model = new HasaInputViewModel();
            var emtea = await _emteaService.GetEmteaTable(1);
            var user = GetCurrentUser();
            if(user!=null)
            {
                var cities=  _cityService.ListAllCities().Veri;
                model.Cities = cities.Where(x=>x.SubeId==user.SubeId).ToList();
            }            
            model.Emteas = emtea.Veri;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DataInputRice(DataInputs dataInputs)
        {
            return View();
        }

    }
}