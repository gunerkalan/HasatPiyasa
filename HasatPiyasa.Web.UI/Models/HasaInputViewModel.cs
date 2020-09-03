using HasatPiyasa.Core.Entities;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;


namespace HasatPiyasa_Web_UI.Models
{
    public class HasaInputViewModel
    {
        public Emteas Emteas { get; set; }
        public EmteaGroups EmteaGroups { get; set; }
        public EmteaTypeGroups EmteaTypeGroups { get; set; }
        public EmteaTypes EmteaTypes { get; set; }
        public List<SubeCities>  Cities { get; set; }
        public List<SubeCityDto>  CitiesRapor { get; set; }
        public List<EmteaTypes> EmteaTypesRapor { get; set; }
        public List<Bolges> Bolges { get; set; }
        public List<DataInputs>  DataInputs { get; set; }
        public List<DateTime>  DateInputs { get; set; }
        public bool HaveTodayInputData { get; set; }
        public int SelectedCityId { get; set; }

    }
}
