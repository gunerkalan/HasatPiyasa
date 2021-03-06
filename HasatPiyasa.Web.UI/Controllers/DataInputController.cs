﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo.Metadata.Helpers;
using HasatPiyasa.Business;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.FilterAttributes;
using HasatPiyasa.Web.UI.Models;
using HasatPiyasa_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class DataInputController : BaseController
    {
        private IDataInputService _dataInputService;
        private IEmteaService _emteaService;
        private ICityService _cityService;
        private ISubeCityService _subeCityService;
        private readonly IFormDataInputService _formDataInputService;

        public DataInputController(IDataInputService dataInputService, IEmteaService emteaService, ICityService cityService, ISubeCityService subeCityService, IFormDataInputService formDataInputService)
        {
            _dataInputService = dataInputService;
            _emteaService = emteaService;
            _cityService = cityService;
            _subeCityService = subeCityService;
            _formDataInputService = formDataInputService;
        }

        [HttpGet]
        public async Task<ActionResult> DataInputRice(int cityId = 0)
        {

            var model = new HasaInputViewModel();
            int em = (int)Core.Utilities.Enums.DataInput.Data.Rice;
            var emtea = await _emteaService.GetEmteaTable(em, cityId);
            var IsLock = false;

            var user = GetCurrentUser();
            if (user != null)
            {
                var cities = await _subeCityService.GetSubeCityGTable(user.SubeId);
                model.Cities = cities.Veri.Where(x => x.SubeId == user.SubeId).OrderBy(x => x.Id).ToList();
            }
            model.Emteas = emtea.Veri.Emteas;
            model.DataInputs = emtea.Veri.DataInputs;

            if (cityId == 0)
            {

                cityId = GetCurrentUser().Sube.SubeCities.FirstOrDefault().CityId;
                model.SelectedCityId = cityId;
                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;

                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }
            else
            {
                model.SelectedCityId = cityId;

                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                      
                    }
                    
                }

                //model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }


            model.DataInputs = emtea.Veri.DataInputs;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DataInputRice(List<DataInputs> dataInputs)
        {
            var user = GetCurrentUser();


            //var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, dataInputs.FirstOrDefault().CityId, user.SubeId, user.UserId);


            dataInputs.ForEach(x =>
            {
                x.SubeId = user.SubeId;
                x.AlimYear = DateTime.Now.Year;
                x.EmteaId = (int)Core.Utilities.Enums.DataInput.Data.Rice;
                x.IsActive = true;
            });

            if (dataInputs.Count > 0)
            {
                var cityId = dataInputs.FirstOrDefault().CityId;
                var response = await _dataInputService.CreateDataInputRange(dataInputs, cityId, user.SubeId, user.UserId);
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

        [HttpGet]
        public ActionResult DataInputRiceList()
        {
            DataInputRiceListModel model = new DataInputRiceListModel
            {
                FormDataInput = new FormDataInput()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> GetDataInputs(int id)
        {
            var response = await _dataInputService.GetDataInputsTableForm(id);

            return JsonConvert.SerializeObject(response.Veri);

        }

        [HttpGet]
        public async Task<object> DataInputRiceListData()
        {
            var user = GetCurrentUser();
            int ChooseEmteaId = (int)Core.Utilities.Enums.DataInput.Data.Rice;
            if (user.Roles == "Admin")
            {
                var res = await _formDataInputService.GetFormDataInputGTable(null, ChooseEmteaId);

                return JsonConvert.SerializeObject(res.Veri);
            }
            else
            {
                int ChoseeSubeId = user.SubeId;


                var res = await _formDataInputService.GetFormDataInputGTable(ChoseeSubeId, ChooseEmteaId);

                return JsonConvert.SerializeObject(res.Veri);
            }


        }

        [HttpGet]//Hububat Get
        public ActionResult DataImputCereal()
        {
            return View();
        }

        [HttpPost] //Hububat Post
        public ActionResult DataInputCereal(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Mısır Get
        public async Task<ActionResult> DataInputCorn(int cityId = 0)
        {
            var model = new HasaInputViewModel();
            int em = (int)Core.Utilities.Enums.DataInput.Data.Corn;
            var emtea = await _emteaService.GetEmteaTable(em, cityId);
            var user = GetCurrentUser();
            if (user != null)
            {
                var cities = await _subeCityService.GetSubeCityGTable(user.SubeId);
                model.Cities = cities.Veri.Where(x => x.SubeId == user.SubeId).OrderBy(x => x.Id).ToList();
            }
            model.Emteas = emtea.Veri.Emteas;

            if (cityId == 0)
            {

                cityId = GetCurrentUser().Sube.SubeCities.FirstOrDefault().CityId;
                model.SelectedCityId = cityId;
                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;

                if (Inputs.Veri != null)
                {
                    ViewBag.AddTimeValue = Inputs.Veri.AddedTime.ToShortDateString();
                }

                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }
            else
            {
                model.SelectedCityId = cityId;

                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }

            return View(model);
        }

        [HttpPost] //Mısır Post
        public ActionResult DataInputCorn(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Kırmızı Mercimek Get
        public async Task<ActionResult> DataInputRedLentil(int cityId = 0)
        {
            var model = new HasaInputViewModel();
            int em = (int)Core.Utilities.Enums.DataInput.Data.RedLentil;
            var emtea = await _emteaService.GetEmteaTable(em, cityId);
            var user = GetCurrentUser();
            if (user != null)
            {
                var cities = await _subeCityService.GetSubeCityGTable(user.SubeId);
                model.Cities = cities.Veri.Where(x => x.SubeId == user.SubeId).OrderBy(x => x.Id).ToList();
            }
            model.Emteas = emtea.Veri.Emteas;

            if (cityId == 0)
            {

                cityId = GetCurrentUser().Sube.SubeCities.FirstOrDefault().CityId;
                model.SelectedCityId = cityId;
                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;

                if (Inputs.Veri != null)
                {
                    ViewBag.AddTimeValue = Inputs.Veri.AddedTime.ToShortDateString();
                }

                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }
            else
            {
                model.SelectedCityId = cityId;

                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }

            return View(model);
        }

        [HttpPost] //Kırmızı Mercimek Post
        public ActionResult DataInputRedLentil(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Yeşil Mercimek Get
        public async Task<ActionResult> DataInputGreenLentil(int cityId = 0)
        {
            var model = new HasaInputViewModel();
            int em = (int)Core.Utilities.Enums.DataInput.Data.GreenLentil;
            var emtea = await _emteaService.GetEmteaTable(em, cityId);
            var user = GetCurrentUser();
            if (user != null)
            {
                var cities = await _subeCityService.GetSubeCityGTable(user.SubeId);
                model.Cities = cities.Veri.Where(x => x.SubeId == user.SubeId).OrderBy(x => x.Id).ToList();
            }
            model.Emteas = emtea.Veri.Emteas;

            if (cityId == 0)
            {

                cityId = GetCurrentUser().Sube.SubeCities.FirstOrDefault().CityId;
                model.SelectedCityId = cityId;
                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;

                if (Inputs.Veri != null)
                {
                    ViewBag.AddTimeValue = Inputs.Veri.AddedTime.ToShortDateString();
                }

                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }
            else
            {
                model.SelectedCityId = cityId;

                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }

            return View(model);
        }

        [HttpPost] //Yeşil Mercimek Post
        public ActionResult DataInputGreenLentil(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Kuru Fasulye Get
        public async Task<ActionResult> DataInputBean(int cityId = 0)
        {
            var model = new HasaInputViewModel();
            int em = (int)Core.Utilities.Enums.DataInput.Data.Bean;
            var emtea = await _emteaService.GetEmteaTable(em, cityId);

            var user = GetCurrentUser();
            if (user != null)
            {
                var cities = await _subeCityService.GetSubeCityGTable(user.SubeId);
                model.Cities = cities.Veri.Where(x => x.SubeId == user.SubeId).OrderBy(x => x.Id).ToList();
            }
            model.Emteas = emtea.Veri.Emteas;
            model.DataInputs = emtea.Veri.DataInputs;

            if (cityId == 0)
            {

                cityId = GetCurrentUser().Sube.SubeCities.FirstOrDefault().CityId;
                model.SelectedCityId = cityId;
                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;

                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }
            else
            {
                model.SelectedCityId = cityId;

                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                //model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }


            model.DataInputs = emtea.Veri.DataInputs;
            return View(model);
        }

        [HttpPost] //Kuru Fasulye Post
        public ActionResult DataInputBean(List<DataInputs> dataInputs)
        {
            return View();
        }

        [HttpGet]//Nohut Get
        public async Task<ActionResult> DataInputChickPea(int cityId = 0)
        {
            var model = new HasaInputViewModel();
            int em = (int)Core.Utilities.Enums.DataInput.Data.ChickPea;
            var emtea = await _emteaService.GetEmteaTable(em, cityId);

            var user = GetCurrentUser();
            if (user != null)
            {
                var cities = await _subeCityService.GetSubeCityGTable(user.SubeId);
                model.Cities = cities.Veri.Where(x => x.SubeId == user.SubeId).OrderBy(x => x.Id).ToList();
            }
            model.Emteas = emtea.Veri.Emteas;
            model.DataInputs = emtea.Veri.DataInputs;

            if (cityId == 0)
            {

                cityId = GetCurrentUser().Sube.SubeCities.FirstOrDefault().CityId;
                model.SelectedCityId = cityId;
                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;

                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }
            else
            {
                model.SelectedCityId = cityId;

                var Inputs = await _formDataInputService.GetFormDataInputTable(DateTime.Today, cityId, user.SubeId, user.UserId);

                if (Inputs.Veri != null)
                {
                    if ((Inputs.Veri.AddedTime.Date == DateTime.Today || Inputs.Veri.UpdatedTime == DateTime.Today) && Inputs.Veri.IsLock)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                //model.DataInputs = Inputs.Veri != null ? _dataInputService.ListAllDataInputs().Veri.Where(x => x.FormDataInputId == Inputs.Veri.Id).ToList() : null;
                model.HaveTodayInputData = model.DataInputs != null ? true : false;

            }


            model.DataInputs = emtea.Veri.DataInputs;
            return View(model);
        }

        [HttpPost] //Nohut Post
        public ActionResult DataInputChickPea(List<DataInputs> dataInputs)
        {
            return View();
        }


    }
}