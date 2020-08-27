using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface ITuikService
    {
        Task<NIslemSonuc<Tuiks>> CreateTuikData(Tuiks tuik);
        Task<NIslemSonuc<Tuiks>> CreateTuikDataForCity(Tuiks tuik);
        NIslemSonuc<List<Tuiks>> ListAllTuikData();
        NIslemSonuc<Tuiks> GetTuikData(int id);
        Task<NIslemSonuc<Tuiks>> UpdateTuikData(Tuiks tuik);
        Task<NIslemSonuc<Tuiks>> GetTuikTable(int value);
        Task<NIslemSonuc<List<TuikSubeDto>>> GetTuikSubeGTable();
        Task<NIslemSonuc<List<TuikCityDto>>> GetTuikCityGTable();
        Task<NIslemSonuc<TuikSubeEditDto>> GetTuikSubeAsync(int id);
        Task<NIslemSonuc<TuikSubeEditDto>> GetTuikCityAsync(int id);
        Task<NIslemSonuc<bool>> DeleteTuik(Tuiks tuik);
        Task<NIslemSonuc<TuikSubeDto>> GetDetail(int id);
        Task<NIslemSonuc<TuikSubeDto>> GetDetailTuikCity(int id);
    }
}
