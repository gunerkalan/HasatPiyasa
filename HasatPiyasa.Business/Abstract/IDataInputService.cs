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
        Task<NIslemSonuc<DataInputs>> CreateDataInputRange(List<DataInputs> dataInputs,int cityid, int subeid);
        NIslemSonuc<List<DataInputs>> ListAllDataInputs();
        NIslemSonuc<DataInputs> GetDataInput(int id);
        Task<NIslemSonuc<DataInputs>> UpdateDataInput(DataInputs dataInput);
        Task<NIslemSonuc<DataInputs>> GetDataInputTable(int value);
    }
}
