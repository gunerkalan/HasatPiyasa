using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DevExpress.DirectX.Common.Direct2D;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class AdminController : BaseController
    {
        private IEmteaService _emteaService;
        private IEmteaGroupService _emteaGroupService;
        private IEmteaTypeService _emteaTypeService;
        private IEmteaTypeGroupService _emteaTypeGroupService;
        private ITuikService _tuikService;
        private ISubeService _subeService;
        private IUserService _userService;

        public AdminController(IEmteaService emteaService, IEmteaGroupService emteaGroupService, IEmteaTypeService emteaTypeService, IEmteaTypeGroupService emteaTypeGroupService, ITuikService tuikService, ISubeService subeService, IUserService userService)
        {
            _emteaService = emteaService;
            _emteaGroupService = emteaGroupService;
            _emteaTypeService = emteaTypeService;
            _emteaTypeGroupService = emteaTypeGroupService;
            _tuikService = tuikService;
            _subeService = subeService;
            _userService = userService;
        }

        #region Emtea işlemleri

        [HttpGet]
        public ActionResult EmteaList()
        {
            EmteaModel model = new EmteaModel
            {
                Emtea = new Emteas()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> EmteaListData()
        {
            var res = await _emteaService.GetEmteaGTable();

            return JsonConvert.SerializeObject(res.Veri);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmtea(Emteas emtea)
        {
            emtea.IsActive = true;
            emtea.AddedTime = DateTime.Now;
            var sonuc = await _emteaService.CreateEmtea(emtea);
            if(sonuc.BasariliMi)
            {
                return Json(new { success = true ,messages=sonuc.Mesaj});
            }
            else
            {
                return Json(new { success = false, messages = sonuc.Mesaj });
            }
        }


        #endregion

        #region Emtea Grup işlemleri

        [HttpGet]
        public ActionResult EmteaGroupList()
        {
            EmteaGoupAddModel model = new EmteaGoupAddModel
            {
                EmteaGroup = new EmteaGroups(),
                Emteas = LoadEmteas()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> EmteaGroupListData()
        {
            var res = await _emteaGroupService.GetEmteaGroupTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        private List<SelectListItem> LoadEmteas()
        {
            List<SelectListItem> emteas = (from emtea in _emteaService.ListAllEmteas().Veri
                                           select new SelectListItem
                                           {
                                               Value = emtea.Id.ToString(),
                                               Text = emtea.EmteaName
                                           }
                        ).ToList();
            return emteas;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmteaGroup(EmteaGroups emteaGroups)
        {
            emteaGroups.IsActive = true;
            emteaGroups.AddedTime = DateTime.Now;
            var sonuc = await _emteaGroupService.CreateEmteaGroup(emteaGroups);
            if (sonuc.BasariliMi)
            {
                return Json(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return Json(new { success = false, messages = sonuc.Mesaj });
            }
        }

        #endregion

        #region EmteaTip işlemleri

        [HttpGet]
        public ActionResult EmteaTypeList()
        {
            var model = new EmteTypeAddModel
            {
                EmteaTypes = new EmteaTypes()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> EmteaTypeListData()
        {
            var res = await _emteaTypeService.GetEmteaTypeGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        #endregion

        #region EmteaTypeGroup işlemleri

        [HttpGet]
        public ActionResult EmteaTypeGroupList()
        {
            var model = new EmteaTypeGroupAddModel
            {
                EmteaTypeGroups = new EmteaTypeGroups()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> EmteaTypeGroupListData()
        {
            var res = await _emteaTypeGroupService.GetEmteatypeGroupGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        #endregion

        #region Tuik İşlemleri

        [HttpGet]
        public ActionResult TuikSubeList()
        {
            TuikAddModel model = new TuikAddModel
            {
                Tuik = new Tuiks()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> TuikSubeListData()
        {
            var res = await _tuikService.GetTuikSubeGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        [HttpGet]
        public async Task<object> TuikCityListData()
        {
            var res = await _tuikService.GetTuikCityGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }


        [HttpGet]
        public ActionResult TuikCityList()
        {
            TuikAddModel model = new TuikAddModel
            {
                Tuik = new Tuiks()
            };

            return View(model);
        }


        #endregion

        #region Şube işlemleri

        [HttpGet]
        public ActionResult SubeList()
        {
            SubeAddModel model = new SubeAddModel
            {
                Sube = new Subes()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> SubeListData()
        {
            var res = await _subeService.GetSubeGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        #endregion

        #region Kullanıcı İşlemleri

        [HttpGet]
        public ActionResult UserList()
        {
            UserAddModel model = new UserAddModel
            {
                User = new Users()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> UserListData()
        {
            var res = await _userService.GetUserGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        #endregion

    }
}
