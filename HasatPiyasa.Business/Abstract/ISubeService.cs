﻿using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface ISubeService
    {
        Task<NIslemSonuc<Subes>> CreateSube(Subes sube);
        NIslemSonuc<List<Subes>> ListAllSubes();
        NIslemSonuc<Subes> GetSube(int id);
        Task<NIslemSonuc<Subes>> UpdateSube(Subes sube);
        Task<NIslemSonuc<Subes>> GetSubeTable(int value);
        Task<NIslemSonuc<List<SubeDto>>> GetSubeGTable();
        Task<NIslemSonuc<List<SubeFormDataWDataInput>>> GetSubeGTableWithFormDatas(int emteaid);
        NIslemSonuc<List<Subes>> GetSubesByBolges(string[] bolges);
        Task<NIslemSonuc<List<SubeCityDto>>> GetSbGTable();
    }
}
