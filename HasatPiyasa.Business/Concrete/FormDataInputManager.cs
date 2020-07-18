using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
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
    }
}
