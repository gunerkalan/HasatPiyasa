using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface ICityService
    {
        Task<NIslemSonuc<Cities>> CreateCity(Cities city);
        NIslemSonuc<List<Cities>> ListAllCities();
        NIslemSonuc<Cities> GetCity(int id);
        Task<NIslemSonuc<Cities>> UpdateCity(Cities city);
        Task<NIslemSonuc<Cities>> GetCityTable(int value);
       
    }
}
