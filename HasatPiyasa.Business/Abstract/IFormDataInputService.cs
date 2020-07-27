using HasatPiyasa.Core.Entities;
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
        Task<NIslemSonuc<List<FormDataInput>>> GetFormDataGTable();
        Task<NIslemSonuc<List<FormDataReportDto>>> GetReporFormDataGTable(int id);
        Task<NIslemSonuc<FormDataInput>> UpdateFormData(int emteaid, int subeid, int cityid, bool state, int formid);
    }
}
