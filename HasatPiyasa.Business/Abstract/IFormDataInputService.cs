using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IFormDataInputService
    {
        Task<NIslemSonuc<FormDataInput>> CreateFormDataInput(FormDataInput formDataInput);
        Task<NIslemSonuc<FormDataInput>> GetFormDataInputTable(DateTime date , int cityId);
    }
}
