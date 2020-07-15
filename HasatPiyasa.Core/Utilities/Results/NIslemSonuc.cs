using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Core.Utilities.Results
{
    public class NIslemSonuc
    {
        public bool BasariliMi { get; set; }
        public string Mesaj { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
    public class NIslemSonuc<T> : NIslemSonuc
    {
        public T Veri { get; set; }

    }
}
