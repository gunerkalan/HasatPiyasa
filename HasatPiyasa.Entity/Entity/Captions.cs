using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HasatPiyasa.Entity.Entity
{
    public class Captions : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int EmteaTypeId { get; set; }
        public int MainCaptionCount { get; set; }
        public int LeftMainCaptionCount { get; set; }
        public int StandartCaptionCount { get; set; }
        public int NaturalDataInputCaptionCount { get; set; }
        public string NaturalDataInputCaption { get; set; }
        public int ToptanPiyasaDataInputCaptionCount { get; set; }
        public string ToptanPiyasaDataInputCaption { get; set; }
        public int PerakendeDataInputCaptionCount { get; set; }
        public string PerakendeDataInputCaption { get; set; }

        public virtual EmteaTypes EmteaTypes { get; set; }

    }
}
