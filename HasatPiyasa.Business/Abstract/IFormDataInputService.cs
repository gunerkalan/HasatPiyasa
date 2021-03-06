﻿using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IFormDataInputService:IFormDataInputDal
    {
        Task<NIslemSonuc<FormDataInput>> CreateFormDataInput(FormDataInput formDataInput);
        Task<NIslemSonuc<FormDataInput>> GetFormDataInputTable(DateTime date , int cityId, int SubeId, int UserId);
        Task<NIslemSonuc<List<FormDataInput>>> GetFormDataGTable();
        Task<NIslemSonuc<List<FormDataInput>>> GetFormDataGTableForDate(int Emtea, DateTime Date);
        Task<NIslemSonuc<List<FormDataInputDto>>> GetFormDataGTableForOnlyDate(DateTime Date);
        Task<NIslemSonuc<List<DateTime>>> GetReporFormDataGTable(int id);
        Task<NIslemSonuc<FormDataInput>> UpdateFormData(int emteaid, int subeid, int cityid, bool state, int formid);
        Task<NIslemSonuc<List<FormDataInputDto>>> GetFormDataInputGTable(int? SubeId, int EmteaId);
    }
}
