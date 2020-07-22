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

        private List<SelectListItem> LoadEmteaGroups()
        {
            var emteas = LoadEmteas();
            var emteagroups = new List<SelectListItem>();

            _emteaGroupService.ListAllEmteaGroups().Veri
                                                  .Where(s => s.EmteaId == int.Parse(emteas[0].Value)).ToList().
                                                  ForEach(s => emteagroups.Add(new SelectListItem
                                                  {
                                                      Text = s.GroupName,
                                                      Value = s.Id.ToString()
                                                  }));
            return emteagroups;
        }

        private List<SelectListItem> LoadEmteatypes()
        {
            var emteagorups = LoadEmteaGroups();
            var emteatypes = new List<SelectListItem>();

            _emteaTypeService.ListAllEmteType().Veri.
                                               Where(s => s.EmteaGroupId == int.Parse(emteagorups[0].Value)).ToList().
                                               ForEach(s => emteatypes.Add(new SelectListItem
                                               {
                                                   Text = s.EmteaTypeName,
                                                   Value = s.Id.ToString()
                                               }));
            return emteatypes;
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

        private List<SelectListItem> LoadSubes()
        {
            List<SelectListItem> subes = (from sube in _subeService.ListAllSubes().Veri
                                          select new SelectListItem
                                          {
                                              Value = sube.Id.ToString(),
                                              Text = sube.SubeName
                                          }
                        ).ToList();
            return subes;
        }

        [HttpGet]
        public ActionResult TuikSubeList()
        {
            TuikAddModel model = new TuikAddModel
            {
                Tuik = new Tuiks(),
                Subes = LoadSubes(),
                Emteas = LoadEmteas(),
                EmteaGroups = LoadEmteaGroups(),
                EmteaTypes = LoadEmteatypes()
            };

            return View(model);
        }

        public JsonResult ChooseEmteaGrup(string emteaid)
        {
            var emteagoups = _emteaGroupService.ListAllEmteaGroups().Veri.AsEnumerable().Where(s => s.EmteaId == int.Parse(emteaid)).Select
                (s => new
                {
                    id = s.Id,
                    EmteaGrupName = s.GroupName
                }).ToList();

            return Json(emteagoups); 
        }

        public JsonResult ChooseEmteaType(string emteagroupid)
        {
            var emteatypes = _emteaTypeService.ListAllEmteType().Veri.AsEnumerable().Where(s => s.EmteaGroupId == int.Parse(emteagroupid)).Select(s => new
            {
                id=s.Id,
                EmteaTpeName = s.EmteaTypeName

            }).ToList();

            return Json(emteatypes);
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

        [HttpPost]
        public async Task<ActionResult> CreateTuikSubeData(Tuiks subetuik)
        {
            var user = GetCurrentUser();
            subetuik.AddUserId = user.UserId;
            subetuik.IsCity = false;
            subetuik.IsActive = true;
            subetuik.AddedTime = DateTime.Now;
            subetuik.TuikYear = DateTime.Now.Year - 1;
            subetuik.GuessYear = DateTime.Now.Year;

            var sonuc = await _tuikService.CreateTuikData(subetuik);
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
