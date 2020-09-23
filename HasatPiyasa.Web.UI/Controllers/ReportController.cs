using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Core.Utilities.Enums;
using HasatPiyasa_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class ReportController : BaseController
    {
        private IDataInputService _dataInputService;
        private IEmteaService _emteaService;
        private ICityService _cityService;
        private ISubeCityService _subeCityService;
        private IBolgeService _bolgeService;
        private ISubeService _subeService;
        private readonly IFormDataInputService _formDataInputService;
        private IEmteaTypeService _emteaTypeService;

        public ReportController(IDataInputService dataInputService, IEmteaService emteaService, IEmteaTypeService emteaTypeService, ICityService cityService, ISubeCityService subeCityService, IBolgeService bolgeService, ISubeService subeService, IFormDataInputService formDataInputService)
        {
            _dataInputService = dataInputService;
            _emteaService = emteaService;
            _cityService = cityService;
            _subeCityService = subeCityService;
            _bolgeService = bolgeService;
            _subeService = subeService;
            _formDataInputService = formDataInputService;
            _emteaTypeService = emteaTypeService;

        }

        [HttpGet]
       public async Task<ActionResult> RiceGeneralReportBySube()
        {
            var model = new  HasaInputViewModel();
            var _cities = await _subeService.GetSbGTable();            
            model.CitiesRapor = _cities.Veri.Distinct().OrderBy(u=>u.SubeName).ToList();
           var _dates =await  _formDataInputService.GetTable();
            model.DateInputs=_dates.Select(x => x.AddedTime.Date).Distinct().ToList();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice,0);
            model.Emteas = emtea.Veri.Emteas;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RiceGeneralReportBySubePartial(string[] dates,string[] cities,bool allDate ,bool allcities)
        {
            var model = new HasaInputViewModel();          
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 1);
            var dataInputs = _dataInputService.ListAllDataInputs().Veri;
            model.Emteas = emtea.Veri.Emteas;             
            foreach (var item in model.Emteas.EmteaGroups)
            {
                foreach (var emteaTypes in item.EmteaTypes)
                {
                    if(!allcities && !allDate)
                    {
                        if (dates.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x => dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                        }

                        if (cities.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x => cities.Contains(x.SubeId.ToString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                        }

                        if (cities.Count() > 0 && dates.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x => cities.Contains(x.SubeId.ToString()) && dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                        }
                    }
                    else
                    {
                        if (allcities)
                        {
                            if (dates.Count() > 0)
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                            else
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                        }

                        if (allDate)
                        {
                            if (cities.Count() > 0)
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => cities.Contains(x.SubeId.ToString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                            else
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                        }
                    }
                      
                    if(allcities && allDate)
                    {
                        emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                    }
                    
                }

            }
            return  PartialView (model);
        }

        [HttpGet]
        public async Task<ActionResult> RiceGeneralReportByBolge()
        {
            var model = new HasaInputViewModel();
            var _bolges =_bolgeService.ListAllBolges();
            model.Bolges = _bolges.Veri;
            var _dates = await _formDataInputService.GetTable();
            model.DateInputs = _dates.Select(x => x.AddedTime.Date).Distinct().ToList();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 0);
            model.Emteas = emtea.Veri.Emteas;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RiceGeneralReportByBolgePartial(string[] dates, string[] cities, bool allDate, bool allcities)
        {
            var model = new HasaInputViewModel();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 1);           
            var dataInputs = _dataInputService.ListAllDataInputs().Veri;
            var bolges = _subeService.GetSubesByBolges(cities).Veri;
            model.Emteas = emtea.Veri.Emteas;
            foreach (var item in model.Emteas.EmteaGroups)
            {
                foreach (var emteaTypes in item.EmteaTypes)
                {
                    if (!allcities && !allDate)
                    {
                        if (dates.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x => dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                        }

                        if (cities.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x=>  bolges.Where(b => b.Id == x.SubeId).Count() > 0 ).Where(x =>  x.EmteaTypeId == emteaTypes.Id).ToList();
                        }

                        if (cities.Count() > 0 && dates.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x => bolges.Where(b => b.Id == x.SubeId).Count() > 0 && dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                        }
                    }
                    else
                    {
                        if (allcities)
                        {
                            if (dates.Count() > 0)
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                            else
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                        }

                        if (allDate)
                        {
                            if (cities.Count() > 0)
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => bolges.Where(b => b.Id == x.SubeId).Count() > 0 && x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                            else
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                        }
                    }

                    if (allcities && allDate)
                    {
                        emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                    }

                }

            }
            return PartialView(model);
        }

        [HttpGet]
        public async Task<ActionResult> RiceGeneralReportByCity()
        {
            var model = new HasaInputViewModel();
            var _cities = await _subeCityService.GetSbCityGTable();
            model.CitiesRapor = _cities.Veri.OrderBy(u=>u.CityName).ToList();
            var _dates = await _formDataInputService.GetTable();
            model.DateInputs = _dates.Select(x => x.AddedTime.Date).Distinct().ToList();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 0);
            model.Emteas = emtea.Veri.Emteas;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RiceGeneralReportByCityPartial(string[] dates, string[] cities, bool allDate, bool allcities)
        {
            var model = new HasaInputViewModel();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 1);
            var dataInputs = _dataInputService.ListAllDataInputs().Veri;
            model.Emteas = emtea.Veri.Emteas;
            foreach (var item in model.Emteas.EmteaGroups)
            {
                foreach (var emteaTypes in item.EmteaTypes)
                {
                    if (!allcities && !allDate)
                    {
                        if (dates.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x => dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                        }

                        if (cities.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x => cities.Contains(x.CityId.ToString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                        }

                        if (cities.Count() > 0 && dates.Count() > 0)
                        {
                            emteaTypes.DataInputs = dataInputs.Where(x => cities.Contains(x.CityId.ToString()) && dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                        }
                    }
                    else
                    {
                        if (allcities)
                        {
                            if (dates.Count() > 0)
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => dates.Contains(x.AddedTime.ToShortDateString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                            else
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                        }

                        if (allDate)
                        {
                            if (cities.Count() > 0)
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => cities.Contains(x.SubeId.ToString()) && x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                            else
                            {
                                emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                            }
                        }
                    }

                    if (allcities && allDate)
                    {
                        emteaTypes.DataInputs = dataInputs.Where(x => x.EmteaTypeId == emteaTypes.Id).ToList();
                    }

                }

            }
            return PartialView(model);
        }

        [HttpGet]
        public async Task<ActionResult> RiceMarketReportByCity()
        {
            var model = new HasaInputViewModel();
            var _cities = await _subeCityService.GetSbCityGTable();
            model.CitiesRapor = _cities.Veri.ToList();
            var _dates = await _formDataInputService.GetTable();
            model.DateInputs = _dates.Select(x => x.AddedTime.Date).Distinct().ToList();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 0);
            model.Emteas = emtea.Veri.Emteas;
            model.EmteaTypesRapor = _emteaTypeService.GetEmteaTypesForEmtea((int)Core.Utilities.Enums.DataInput.Data.Rice).Result.Veri;
            ViewBag.dates = 2;
            ViewBag.emteatypes = 3;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> RiceMarketReportByCityPartial(string[] dates, string[] emteatypes)
        {
            var model = new HasaInputViewModel();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 1);
            var dataInputs = _dataInputService.ListAllDataInputs().Veri;
            // model.Emteas = emtea.Veri;
            ViewData["dates"] = dates;
            ViewData["emteatypes"] = emteatypes;
            return PartialView(model);
        }



        public async  Task<object> RiceMarketReportByCityData(string[] dates, string[] emteatypes)
        {
            //var model = new HasaInputViewModel();
            //var emtea = await _emteaService.GetEmteaTable(1, 1);
            dates = dates[0].Split(",");
            emteatypes = emteatypes[0].Split(",");       
            var dataInputtask =_dataInputService.ListDataInputsForCityMarket(dates, emteatypes);
            var dataInputs = dataInputtask.Veri;
            var _cities = await _subeCityService.GetSbCityGTable();
            //model.Emteas = emtea.Veri;

            return JsonConvert.SerializeObject(dataInputs);
        }
        public async Task<ActionResult> RiceMarketReportBySube()
        {
            var model = new HasaInputViewModel();
            var _cities = await _subeCityService.GetSbCityGTable();
            model.CitiesRapor = _cities.Veri.ToList();
            var _dates = await _formDataInputService.GetTable();
            model.DateInputs = _dates.Select(x => x.AddedTime.Date).Distinct().ToList();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 0);
            model.Emteas = emtea.Veri.Emteas;
            model.EmteaTypesRapor = _emteaTypeService.GetEmteaTypesForEmtea((int)Core.Utilities.Enums.DataInput.Data.Rice).Result.Veri;
            ViewBag.dates = 2;
            ViewBag.emteatypes = 3;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> RiceMarketReportBySubePartial(string[] dates, string[] emteatypes)
        {
            var model = new HasaInputViewModel();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 1);
            var dataInputs = _dataInputService.ListAllDataInputs().Veri;
            // model.Emteas = emtea.Veri;
            ViewData["dates"] = dates;
            ViewData["emteatypes"] = emteatypes;
            return PartialView(model);
        }



        public async Task<object> RiceMarketReportBySubeData(string[] dates, string[] emteatypes)
        {
            //var model = new HasaInputViewModel();
            //var emtea = await _emteaService.GetEmteaTable(1, 1);
            dates = dates[0].Split(",");
            emteatypes = emteatypes[0].Split(",");
            var dataInputtask = _dataInputService.ListDataInputsForSubeMarket(dates, emteatypes);
            var dataInputs = dataInputtask.Veri;
            var _cities = await _subeCityService.GetSbCityGTable();
            //model.Emteas = emtea.Veri;

            return JsonConvert.SerializeObject(dataInputs);
        }
        public async Task<ActionResult> RiceMarketReportBySubeBolge()
        {
            var model = new HasaInputViewModel();
            var _cities = await _subeCityService.GetSbCityGTable();
            model.CitiesRapor = _cities.Veri.ToList();
            var _dates = await _formDataInputService.GetTable();
            model.DateInputs = _dates.Select(x => x.AddedTime.Date).Distinct().ToList();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 0);
            model.Emteas = emtea.Veri.Emteas;
            model.EmteaTypesRapor = _emteaTypeService.GetEmteaTypesForEmtea((int)Core.Utilities.Enums.DataInput.Data.Rice).Result.Veri;
            ViewBag.dates = 2;
            ViewBag.emteatypes = 3;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> RiceMarketReportBySubeBolgePartial(string[] dates, string[] emteatypes)
        {
            var model = new HasaInputViewModel();
            var emtea = await _emteaService.GetEmteaTable((int)Core.Utilities.Enums.DataInput.Data.Rice, 1);
            var dataInputs = _dataInputService.ListAllDataInputs().Veri;
            // model.Emteas = emtea.Veri;
            ViewData["dates"] = dates;
            ViewData["emteatypes"] = emteatypes;
            return PartialView(model);
        }

        [HttpGet]
        public async Task<object> ChooseFormDataInput()
        {
            var res = await _formDataInputService.GetReporFormDataGTable((int)DataInput.Data.Rice);
            return JsonConvert.SerializeObject(res.Veri);
        }

    }
}
