using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class ChemicalComposition : Entity
    {
        [DisplayName("Название состава")]
        public new string Title { set; get; }

        public virtual ICollection<ChemicalElement> ChemicalElements { set; get; }
        public virtual ICollection<Sample> Samples { set; get; }
    }
}