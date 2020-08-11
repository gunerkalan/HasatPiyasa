using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IEmteaService
    {
        Task<NIslemSonuc<Emteas>> CreateEmtea(Emteas emtea);
        NIslemSonuc<List<Emteas>> ListAllEmteas();
        NIslemSonuc<Emteas> GetEmtea(int id);
        Task<NIslemSonuc<Emteas>> GetEmteaAsync(int id);
        Task<NIslemSonuc<Emteas>> UpdateEmtea(Emteas emtea);
        Task<NIslemSonuc<Emteas>> GetEmteaTable(int value,int cityId);
        Task<NIslemSonuc<List<EmteaDto>>> GetEmteaGTable();
        Task<NIslemSonuc<bool>> DeleteEmtea(int id);

    }
}
