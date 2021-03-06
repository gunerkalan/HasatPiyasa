﻿using HasatPiyasa.Core.Entities;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IEmteaTypeGroupService
    {
        Task<NIslemSonuc<EmteaTypeGroups>> CreateEmteaTypeGroups(EmteaTypeGroups emteatypegroup);
        NIslemSonuc<List<EmteaTypeGroups>> ListAllEmteTypeGroups();
        NIslemSonuc<EmteaTypeGroups> GetEmteaTypeGroup(int id);
        Task<NIslemSonuc<EmteaTypeGroups>> UpdateEmteaTypeGroup(EmteaTypeGroups emteatypegroup);
        Task<NIslemSonuc<EmteaTypeGroups>> GetEmteaTypeGroupTable(int value);
        Task<NIslemSonuc<List<EmteaTypeGroupDto>>> GetEmteatypeGroupGTable();
        Task<NIslemSonuc<EmteaGroupTypeDto>> GetEmteaGroupTypesAsync(int id);
        Task<NIslemSonuc<bool>> DeleteEmteaTypeGroup(EmteaTypeGroups emteatypegroup);
    }
}
