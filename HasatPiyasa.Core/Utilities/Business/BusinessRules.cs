using HasatPiyasa.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Utilities.Business
{
    public class BusinessRules
    {
        public static NIslemSonuc<bool> Run(params NIslemSonuc<bool>[] logis)
        {
            foreach (var sonuc in logis)
            {
                if (!sonuc.BasariliMi)
                {
                    return sonuc;
                }
            }

            return new NIslemSonuc<bool>
            {
                BasariliMi = true,
            };
        }
    }
}
