﻿using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Constants;
using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Concrete
{
    public class DataInputManager : IDataInputService
    {
        private IDataInputDal _dataInputDal;

        public DataInputManager(IDataInputDal dataInputDal)
        {
            _dataInputDal = dataInputDal;
        }

        public async Task<NIslemSonuc<DataInputs>> CreateDataInput(DataInputs dataInput)
        {
            try
            {
                var addeddatainput = await _dataInputDal.AddAsync(dataInput);

                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = true,
                    Veri = addeddatainput,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }

        public NIslemSonuc<DataInputs> GetDataInput(int id)
        {
            try
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = true,
                    Veri = _dataInputDal.Get(u => u.Id == id)
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<DataInputs>> GetDataInputTable(int value)
        {
            try
            {
                var res = await _dataInputDal.GetTable();

                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = true,
                    Veri = res.AsQueryable().Include(x => x.City).Include(x=>x.Sube).Include(x=>x.EmteaType).Where(x => x.Id == value).ToList().FirstOrDefault()
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public NIslemSonuc<List<DataInputs>> ListAllDataInputs()
        {
            try
            {
                return new NIslemSonuc<List<DataInputs>>
                {
                    BasariliMi = true,
                    Veri = _dataInputDal.GetList().ToList()
                };

            }
            catch (Exception hata)
            {

                return new NIslemSonuc<List<DataInputs>>
                {
                    BasariliMi = true,
                    Mesaj = hata.InnerException.Message
                };
            }
        }

        public async Task<NIslemSonuc<DataInputs>> UpdateDataInput(DataInputs dataInput)
        {
            try
            {
                var updateddatainput = await _dataInputDal.UpdateAsync(dataInput);

                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = true,
                    Veri = updateddatainput,

                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<DataInputs>
                {
                    BasariliMi = false,
                    Mesaj = Messages.ErrorAddSaleOrder,
                    ErrorMessage = hata.Message
                };
            }
        }
    }
}