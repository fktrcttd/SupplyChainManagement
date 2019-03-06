using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class ChemicalElement : Entity
    {
        [DisplayName("'Элемент")]
        public new string Title { set; get; }

        public virtual ICollection<ChemicalComposition> ChemicalCompositions { set; get; }
    }
}