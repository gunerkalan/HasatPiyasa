using HasatPiyasa.Entity.Entity;
using System.Collections.Generic;

namespace HasatPiyasa.Web.UI.Models
{
    public class EmteaAndDataInputDto
    {
        public Emteas Emteas { get; set; }
        public List<DataInputs> DataInputs  { get; set; }
    }
}
