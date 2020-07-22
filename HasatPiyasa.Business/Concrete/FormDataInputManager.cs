using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class FormDataInputManager : IFormDataInputService
    {
        private IFormDataInputDal _formDataInputDal;

        public FormDataInputManager(IFormDataInputDal formDataInputDal)
        {
            _formDataInputDal = formDataInputDal;
        }

        public async Task<NIslemSonuc<FormDataInput>> CreateFormDataInput(FormDataInput formDataInput)
        {
            try
            {
               
                var addedform = await _formDataInputDal.AddAsync(formDataInput);

                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = true,
                    Veri = addedform
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public async Task<NIslemSonuc<FormDataInput>> GetFormDataInputTable(DateTime date,int cityId)
        {
            try
            {
                var res = await _formDataInputDal.GetTable();

                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = true,
                    Veri = res.AsNoTracking().Include(x => x.DataInputs).ThenInclude(x=>x.EmteaType).ThenInclude(x=>x.EmteaGroup).ThenInclude(x=>x.Emtea).
                    Where(x => x.AddedTime==date && x.IsActive && x.IsLock==false && x.CityId==cityId).FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<FormDataInput>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }
    }
}
