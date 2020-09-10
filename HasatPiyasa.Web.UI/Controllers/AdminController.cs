using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DevExpress.DirectX.Common.Direct2D;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.FilterAttributes;
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
        private ICityService _cityService;
        private ISubeCityService _subeCityService;
        private IUserRoleService _userRoleService;
        private IFormDataInputService _formDataInputService;

        public AdminController(IEmteaService emteaService, IEmteaGroupService emteaGroupService, IEmteaTypeService emteaTypeService, IEmteaTypeGroupService emteaTypeGroupService, ITuikService tuikService, ISubeService subeService, IUserService userService, ICityService cityService, ISubeCityService subeCityService, IFormDataInputService formDataInputService, IUserRoleService userRoleService)
        {
            _emteaService = emteaService;
            _emteaGroupService = emteaGroupService;
            _emteaTypeService = emteaTypeService;
            _emteaTypeGroupService = emteaTypeGroupService;
            _tuikService = tuikService;
            _subeService = subeService;
            _userService = userService;
            _subeCityService = subeCityService;
            _formDataInputService = formDataInputService;
            _cityService = cityService;
            _userRoleService = userRoleService;
        }

        #region Emtea işlemleri

        [HttpGet]
        [AuthorizedUser("Admin")]
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

        [HttpPost]
        public async Task<object> GetEmtea(int id)
        {
            var res = await _emteaService.GetEmteaAsync(id);
            return JsonConvert.SerializeObject(res);
        }

        [HttpPost]
        public async Task<object> UpdateEmtea(Emteas emtea)
        {

            emtea.UpdatedTime = DateTime.Now;
            emtea.IsActive = true;


            var sonuc = await _emteaService.UpdateEmtea(emtea);
            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        [HttpPost]
        public async Task<object> DeleteEmtea(Emteas emtea)
        {
            var sonuc = await _emteaService.DeleteEmtea(emtea);

            if(sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
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

        private List<SelectListItem> LoadCities()
        {
            var subes = LoadSubes();
            var cities = new List<SelectListItem>();

            _subeCityService.GetSbCityGTable().Result.Veri.ToList().
                ForEach(s => cities.Add(new SelectListItem
                {
                    Text = s.CityName,
                    Value = s.CityId.ToString()
                }));

            return cities;
        }

        private List<SelectListItem> LoadOnlyCities()
        {
            var cities = new List<SelectListItem>();

            
            _cityService.GetCityGTable().Result.Veri.ToList().ForEach(
                s => cities.Add(new SelectListItem
                {
                    Text = s.CityName,
                    Value = s.CityId.ToString()
                }));

            return cities;
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

        private List<SelectListItem> LoadEmteaTypeGroups()
        {
            var emteatypes = LoadEmteatypes();
            var emteatypegrops = new List<SelectListItem>();

            _emteaTypeGroupService.ListAllEmteTypeGroups().Veri.
                                               Where(s => s.EmteaTypeId == int.Parse(emteatypes[0].Value)).ToList().
                                               ForEach(s => emteatypegrops.Add(new SelectListItem
                                               {
                                                   Text = s.EmteaTypeGroupName,
                                                   Value = s.Id.ToString()
                                               }));
            return emteatypegrops;
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

        public JsonResult EditEmteaGroup(int id)
        {
            if (id > 0)
            {
                var result = _emteaGroupService.GetEmteaGroup(id);

                return Json(result);
            }
            return Json(0);
        }

        [HttpPost]
        public async Task<object> GetEmteaGroup(int id)
        {
            var res = await _emteaGroupService.GetEmteaGroupAsync(id);
            return JsonConvert.SerializeObject(res);
        }


        [HttpPost]
        public async Task<object> UpdateEmteaGroup(EmteaGroups emteagroup)
        {

            emteagroup.UpdatedTime = DateTime.Now;
            emteagroup.IsActive = true;


            var sonuc = await _emteaGroupService.UpdateEmteaGroup(emteagroup);
            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        [HttpPost]
        public async Task<object> DeleteEmteaGroup(EmteaGroups emteagroup)
        {
            var sonuc = await _emteaGroupService.DeleteEmteaGroup(emteagroup);

            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        #endregion

        #region EmteaTip işlemleri


        [HttpGet]
        public ActionResult EmteaTypeList()
        {
            var model = new EmteTypeAddModel
            {
                EmteaTypes = new EmteaTypes(),
                Emteas = LoadEmteas(),
                EmteaGroups = LoadEmteaGroups()
            };

            return View(model);
        }


        [HttpGet]
        public async Task<object> EmteaTypeListData()
        {
            var res = await _emteaTypeService.GetEmteaTypeGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmteaType(EmteaTypes emteaTypes)
        {
            emteaTypes.IsActive = true;
            emteaTypes.AddedTime = DateTime.Now;
            var sonuc = await _emteaTypeService.CreateEmteaType(emteaTypes);
            if (sonuc.BasariliMi)
            {
                return Json(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return Json(new { success = false, messages = sonuc.Mesaj });
            }
        }

        public JsonResult EditEmteaType(int id)
        {
            if (id > 0)
            {
                var result = _emteaTypeService.GetEmteaType(id);

                return Json(result);
            }
            return Json(0);
        }

        [HttpPost]
        public async Task<object> GetEmteaType(int id)
        {
            var res = await _emteaTypeService.GetEmteaTypesAsync(id);
            return JsonConvert.SerializeObject(res);
        }


        [HttpPost]
        public async Task<object> UpdateEmteaType(EmteaTypes emteatype)
        {

            emteatype.UpdatedTime = DateTime.Now;
            emteatype.IsActive = true;


            var sonuc = await _emteaTypeService.UpdateEmteaType(emteatype);
            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        [HttpPost]
        public async Task<object> DeleteEmteaType(EmteaTypes emteatype)
        {
            var sonuc = await _emteaTypeService.DeleteEmteaType(emteatype);

            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        #endregion

        #region EmteaTypeGroup işlemleri

        [HttpGet]
        public ActionResult EmteaTypeGroupList()
        {
            var model = new EmteaTypeGroupAddModel
            {
                EmteaTypeGroups = new EmteaTypeGroups(),
                Emteas = LoadEmteas(),
                EmteaGroups = LoadEmteaGroups(),
                EmteaTypes = LoadEmteatypes()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> EmteaTypeGroupListData()
        {
            var res = await _emteaTypeGroupService.GetEmteatypeGroupGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmteaTypeGroups(EmteaTypeGroups emteaTypeGroups)
        {
            emteaTypeGroups.IsActive = true;
            emteaTypeGroups.AddedTime = DateTime.Now;
            var sonuc = await _emteaTypeGroupService.CreateEmteaTypeGroups(emteaTypeGroups);
            if (sonuc.BasariliMi)
            {
                return Json(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return Json(new { success = false, messages = sonuc.Mesaj });
            }
        }

        [HttpPost]
        public async Task<object> GetEmteaGroupType(int id)
        {
            var res = await _emteaTypeGroupService.GetEmteaGroupTypesAsync(id);
            return JsonConvert.SerializeObject(res);
        }


        [HttpPost]
        public async Task<object> UpdateEmteaTypeGroup(EmteaTypeGroups emteatypegroup)
        {

            emteatypegroup.UpdatedTime = DateTime.Now;
            emteatypegroup.IsActive = true;


            var sonuc = await _emteaTypeGroupService.UpdateEmteaTypeGroup(emteatypegroup);
            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        [HttpPost]
        public async Task<object> DeleteEmteaTypeGroup(EmteaTypeGroups emteatypegroup)
        {
            var sonuc = await _emteaTypeGroupService.DeleteEmteaTypeGroup(emteatypegroup);

            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        #endregion

        #region Tuik İşlemleri

        private List<SelectListItem> LoadSubes()
        {
            var subeler = _subeService.GetSubeGTable().Result;

            List<SelectListItem> subes = (from sube in subeler.Veri
                                          select new SelectListItem
                                          {
                                              Value = sube.Id.ToString(),
                                              Text = sube.SubeName
                                          }
                        ).OrderBy(u=>u.Text).ToList();
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

        public JsonResult ChooseSubeCity(string subeid)
        {
            var cities = _subeCityService.GetSbCityGTable().Result.Veri.AsEnumerable().Where(s => s.SubeId == int.Parse(subeid)).Select
                (s => new
                {
                    id = s.CityId,
                    CitiName = s.CityName
                }).ToList();

            return Json(cities);
        }

        public JsonResult ChooseEmteaType(string emteagroupid)
        {
            var emteatypes = _emteaTypeService.ListAllEmteType().Veri.AsEnumerable().Where(s => s.EmteaGroupId == int.Parse(emteagroupid)).Select(s => new
            {
                id = s.Id,
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

        [HttpPost]
        public async Task<object> GetDetail(int id)
        {
            var res = await _tuikService.GetDetail(id);
            return JsonConvert.SerializeObject(res);
        }

        [HttpPost]
        public async Task<object> GetDetailTuikCity(int id)
        {
            var res = await _tuikService.GetDetailTuikCity(id);
            return JsonConvert.SerializeObject(res);
        }


        [HttpGet]
        public ActionResult TuikCityList()
        {
            TuikCityAddModel model = new TuikCityAddModel
            {
                Tuik = new Tuiks(),
                EmteaGroups = LoadEmteaGroups(),
                Emteas = LoadEmteas(),
                EmteaTypes = LoadEmteatypes(),
                Cities = LoadOnlyCities()
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

        [HttpPost]
        public async Task<ActionResult> CreateTuikCityData(Tuiks citytuik)
        {
            var user = GetCurrentUser();
            citytuik.AddUserId = user.UserId;
            citytuik.IsCity = true;
            citytuik.IsActive = true;
            citytuik.AddedTime = DateTime.Now;
            citytuik.TuikYear = DateTime.Now.Year - 1;
            citytuik.GuessYear = DateTime.Now.Year;

            var sonuc = await _tuikService.CreateTuikDataForCity(citytuik);
            if (sonuc.BasariliMi)
            {
                return Json(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return Json(new { success = false, messages = sonuc.Mesaj });
            }
        }

        [HttpPost]
        public async Task<object> GetTuikSube(int id)
        {
            var res = await _tuikService.GetTuikSubeAsync(id);
            return JsonConvert.SerializeObject(res);
        }

        [HttpPost]
        public async Task<object> GetTuikCity(int id)
        {
            var res = await _tuikService.GetTuikCityAsync(id);
            return JsonConvert.SerializeObject(res);
        }


        [HttpPost]
        public async Task<object> UpdateTuikSube(Tuiks tuik)
        {
            var user = GetCurrentUser();
            tuik.UpdateUserId = user.UserId;
            tuik.UpdatedTime = DateTime.Now;
            tuik.IsActive = true;
            tuik.IsCity = false;
            tuik.TuikYear = DateTime.Now.Year - 1;
            tuik.GuessYear = DateTime.Now.Year;


            var sonuc = await _tuikService.UpdateTuikData(tuik);
            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        [HttpPost]
        public async Task<object> UpdateTuikCity(Tuiks tuik)
        {
            var user = GetCurrentUser();
            tuik.UpdateUserId = user.UserId;
            tuik.UpdatedTime = DateTime.Now;
            tuik.IsActive = true;
            tuik.IsCity = true;
            tuik.TuikYear = DateTime.Now.Year - 1;
            tuik.GuessYear = DateTime.Now.Year;


            var sonuc = await _tuikService.UpdateTuikData(tuik);
            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        [HttpPost]
        public async Task<object> DeleteTuikData(Tuiks tuik)
        {
            var user = GetCurrentUser();
            tuik.UpdateUserId = user.UserId;
            tuik.UpdatedTime = DateTime.Now;

            var sonuc = await _tuikService.DeleteTuik(tuik);

            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
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
                User = new Users(),
                Subes = LoadSubes(),
                UserRoles = LoadUserRoles()
            };

            return View(model);
        }

        private List<SelectListItem> LoadUserRoles()
        {
            var userroles = _userRoleService.GetUserRoleGTable().Result;

            List<SelectListItem> roles = (from role in userroles.Veri
                                          select new SelectListItem
                                          {
                                              Value = role.Id.ToString(),
                                              Text = role.Role
                                          }
                        ).ToList();
            return roles;
        }

        private static string GetUserRole(int id)
        {
            if(id==(int)Core.Utilities.Enums.UserRoles.UserRole.SubeKullanicisi)
            {
                return "SubeUser";
            }
            else if(id == (int)Core.Utilities.Enums.UserRoles.UserRole.RaporKullanicisi)
            {
                return "ReportUser";
            }
            else if(id == (int)Core.Utilities.Enums.UserRoles.UserRole.YetkiliKullanici)
            {
                return "Admin";
            }
            else
            {
                return "SubeUser";
            }
        }


        [HttpGet]
        public async Task<object> UserListData()
        {
            var res = await _userService.GetUserGTable();
            return JsonConvert.SerializeObject(res.Veri);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(Users user)
        {
            user.IsActive = true;
            user.AddedTime = DateTime.Now;
            user.IsDomain = true;
            user.Password = user.SicilNumber;
            user.UserRoleId = user.UserRoleId;
            user.Roles = GetUserRole((int)user.UserRoleId);
            

            var sonuc = await _userService.CreateUser(user);
            if (sonuc.BasariliMi)
            {
                return Json(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return Json(new { success = false, messages = sonuc.Mesaj });
            }
        }

        [HttpPost]
        public async Task<object> GetUser(int id)
        {
            var res = await _userService.GetUserAsync(id);
            return JsonConvert.SerializeObject(res);
        }


        [HttpPost]
        public async Task<object> UpdateHpUser(Users user)
        {

            user.UpdatedTime = DateTime.Now;
            user.IsActive = true;
            user.AddedTime = DateTime.Now;
            user.IsDomain = true;
            user.Password = user.SicilNumber;
            user.UserRoleId = user.UserRoleId;
            user.Roles = GetUserRole((int)user.UserRoleId);


            var sonuc = await _userService.UpdateUser(user);
            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        [HttpPost]
        public async Task<object> DeleteUser(Users user)
        {
            var sonuc = await _userService.DeleteUser(user);

            if (sonuc.BasariliMi)
            {
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }

        #endregion

        #region IsLock 

        [HttpGet]
        public ActionResult IsLockFormData()
        {
            IsLockModel model = new IsLockModel
            {
                Emteas = _emteaService.ListAllEmteas().Veri,
               
            };

            return View(model);
        }

        public async Task<object> GetFormData(int emteaid)
        {
            if (emteaid > 0)
            {
                var formDatas = await _formDataInputService.GetFormDataGTable();

                var fm = formDatas.Veri.Where(x => x.EmteaId == emteaid && x.AddedTime.Date ==DateTime.Today).Select(s => new SetFormDataState
                {
                    FormId = s.Id,
                    CityName = s.City.Name,
                    FormDataDate = s.AddedTime.ToLongDateString() + " " + s.AddedTime.ToShortTimeString(),
                    State = s.IsLock,
                    CityId = s.CityId,
                    SubeId = s.SubeId,
                    SubeName = s.Sube.SubeName

                }).ToList();


                return fm;
            }
            else
            {
                var formData = new List<SetFormDataState>();
                return formData;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateFormDataState(int emteaid, int subeid, int cityid, bool state, int formid)
        {
            var sonuc = await _formDataInputService.UpdateFormData(emteaid, subeid, cityid, state, formid);
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

    }
}
