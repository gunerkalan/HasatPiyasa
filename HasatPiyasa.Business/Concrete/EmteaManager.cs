using Dapper;
using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Business;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class EmteaManager : IEmteaService
    {
        private IEmteaDal _emteaDal;

        public EmteaManager(IEmteaDal emteaDal)
        {
            _emteaDal = emteaDal;
        }

        public async Task<NIslemSonuc<Emteas>> CreateEmtea(Emteas emtea)
        {
            try
            {
                NIslemSonuc sonuc = BusinessRules.Run(CheckEmteaCodeExists(emtea.EmteaCode));
                if(sonuc.BasariliMi)
                {
                    var addedemtea = await _emteaDal.AddAsync(emtea);

                    return new NIslemSonuc<Emteas>
                    {
                        BasariliMi = true,
                        Veri = addedemtea,
                        Mesaj = Messages.SuccessfulyAddEmtea
                    };
                }
                else
                {
                    return new NIslemSonuc<Emteas>
                    {
                        BasariliMi=false,
                        Mesaj = sonuc.Mesaj
                    };
                }

              
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        private NIslemSonuc<bool> CheckEmteaCodeExists(string emteacode)
        {
            if(_emteaDal.Get(p=>p.EmteaCode==emteacode) != null)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorEmteaCode
                };
            }
            return new NIslemSonuc<bool>
            {
                BasariliMi=true
            };
        }

        public NIslemSonuc<Emteas> GetEmtea(int id)
        {
            try
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = true,
                    Veri = _emteaDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<EmteaAndDataInputDto>> GetEmteaTable(int value,int cityId)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();                
                var res = await _emteaDal.GetTable();
                var model = new EmteaAndDataInputDto();
                model.Emteas = res.Include(x => x.EmteaGroups).ThenInclude(x => x.EmteaTypes).ThenInclude(x => x.EmteaTypeGroups).ThenInclude(x => x.EmteaType.Tuiks).FirstOrDefault(x => x.Id == value);

                using(HasatPiyasaContext db = new HasatPiyasaContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();
                    model.DataInputs = conn.Query<DataInputs>($"select * from DataInputs where EmteaId={value}").ToList();
                }

                stopwatch.Stop();
                Debug.WriteLine($"Bitiş (For): {stopwatch.Elapsed}");

                return new NIslemSonuc<EmteaAndDataInputDto>
                {
                    BasariliMi = true,
                    Veri = model
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<EmteaAndDataInputDto>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<Emteas>> ListAllEmteas()
        {
            try
            {
                return new NIslemSonuc<List<Emteas>>
                {
                    BasariliMi = true,
                    Veri = _emteaDal.GetList(u=>u.IsActive).ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<Emteas>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Emteas>> UpdateEmtea(Emteas emtea)
        {
            try
            {
                var updatedemtea = await _emteaDal.UpdateAsync(emtea);

                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = true,
                    Veri = updatedemtea,
                    Mesaj = Messages.EmteaUpdate

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<List<EmteaDto>>> GetEmteaGTable()
        {
            try
            {
                var res = await _emteaDal.GetTable();
                var model = res.Include(x => x.EmteaGroups).Where(x => x.IsActive).ToList();


                var response = model.Select(x => new EmteaDto
                {
                    Id = x.Id,
                    EmteaCode = x.EmteaCode,
                    EmteaName = x.EmteaName,
                    AddedTime = x.AddedTime

                }).ToList();

                return new NIslemSonuc<List<EmteaDto>>
                {
                    BasariliMi = false,
                    Veri = response
                };

            }
            catch (Exception hata)
            {
                return new NIslemSonuc<List<EmteaDto>>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<Emteas>> GetEmteaAsync(int id)
        {
            try
            {
                var res = await _emteaDal.GetTable();
                var model = res.FirstOrDefault(x => x.Id == id);

                return new NIslemSonuc<Emteas>
                {
                    BasariliMi=true,
                    Veri = model
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<Emteas>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<bool>> DeleteEmtea(Emteas emtea)
        {
            try
            {
                var deletedemtea = await _emteaDal.DeleteSoftAsync(emtea);

                return new NIslemSonuc<bool>
                {
                    BasariliMi = true,
                    Veri = deletedemtea,
                    Mesaj = Messages.EmteaDelete

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<bool>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAdd,
                    ErrorMessage = hata.InnerException.Message
                };
            }
        }
    }
}
