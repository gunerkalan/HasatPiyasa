using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IEmteaGroupService
    {
        Task<NIslemSonuc<EmteaGroups>> CreateEmteaGroup(EmteaGroups emteaGroups);
        NIslemSonuc<List<EmteaGroups>> ListAllEmteaGroups();
        NIslemSonuc<EmteaGroups> GetEmteaGroup(int id);
        Task<NIslemSonuc<EmteaGroups>> UpdateEmteaGroup(EmteaGroups emteaGroup);
        Task<NIslemSonuc<EmteaGroups>> GetEmteaGroupTable(int value);
    }
}
