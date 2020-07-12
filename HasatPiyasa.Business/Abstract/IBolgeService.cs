using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IBolgeService
    {
        Task<NIslemSonuc<Bolges>> CreateBolge(Bolges bolges);
        NIslemSonuc<List<Bolges>> ListAllBolges();
        NIslemSonuc<Bolges> GetBolge(int id);
        Task<NIslemSonuc<Bolges>> UpdateBolges(Bolges bolges);
    }
}
