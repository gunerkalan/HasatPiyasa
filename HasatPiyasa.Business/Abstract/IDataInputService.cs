﻿using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IDataInputService
    {
        Task<NIslemSonuc<DataInputs>> CreateDataInput(DataInputs dataInput);
        Task<NIslemSonuc<DataInputs>> CreateDataInputRange(List<DataInputs> dataInputs,int cityid, int subeid, int userid);
        NIslemSonuc<List<DataInputs>> ListAllDataInputs();
        NIslemSonuc<List<ReportDto>> ListDataInputsForCityMarket(string[] dates, string[] emteatypes);
        NIslemSonuc<List<ReportDto>> ListDataInputsForSubeMarket(string[] dates, string[] emteatypes); 
        NIslemSonuc<DataInputs> GetDataInput(int id);
        Task<NIslemSonuc<DataInputs>> UpdateDataInput(DataInputs dataInput);
        Task<NIslemSonuc<DataInputs>> GetDataInputTable(int value);
        Task<NIslemSonuc<List<DataInputTbaleListModelForRiceDto>>> GetDataInputsTableForm(int id);
       
    }
}
