using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface ICaptionService
    {
        Task<NIslemSonuc<Captions>> CreateCaption(Captions caption);
        NIslemSonuc<List<Captions>> ListAllCaptions();
        NIslemSonuc<Captions> GetCaption(int id);
        Task<NIslemSonuc<Captions>> UpdateCaption(Captions emtea);
        Task<NIslemSonuc<Captions>> GetCaptionTable(int value);
    }
}
