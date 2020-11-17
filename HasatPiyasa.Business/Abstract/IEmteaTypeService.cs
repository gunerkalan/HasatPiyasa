using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IEmteaTypeService
    {
        Task<NIslemSonuc<EmteaTypes>> CreateEmteaType(EmteaTypes emteatype);
        NIslemSonuc<List<EmteaTypes>> ListAllEmteType();
  
        NIslemSonuc<EmteaTypes> GetEmteaType(int id);
        Task<NIslemSonuc<EmteaTypes>> UpdateEmteaType(EmteaTypes emteatype);
        Task<NIslemSonuc<EmteaTypes>> GetEmteaTypeTable(int value);
        Task<NIslemSonuc<List<EmteaTypes>>> GetEmteaTypesForEmtea(int EmteaId);
        Task<NIslemSonuc<List<EmteaTypeDto>>> GetEmteaTypeGTable();
        Task<NIslemSonuc<EmteaTypeEditDto>> GetEmteaTypesAsync(int id);
        Task<NIslemSonuc<bool>> DeleteEmteaType(EmteaTypes emteatype);
    }
}
