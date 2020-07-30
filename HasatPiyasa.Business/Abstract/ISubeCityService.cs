using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface ISubeCityService: ISubeCityDal
    {
        Task<NIslemSonuc<List<SubeCities>>> GetSubeCityGTable(int id);
        Task<NIslemSonuc<List<SubeCityDto>>> GetSbCityGTable();
    }
}
