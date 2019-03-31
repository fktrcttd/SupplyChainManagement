using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    [DisplayName("Категория образцов")]
    public class SampleCategory : Entity
    {
        [DisplayName("Наименование категории")]
        public new string Title { set; get; }
        
        [DisplayName("Образцы")]
        public virtual ICollection<Sample> Sample { get; set; }
        
        [DisplayName("Базовые химические элементы")]
        public virtual ICollection<ChemicalElement> ChemicalElements { get; set; }
    }
}