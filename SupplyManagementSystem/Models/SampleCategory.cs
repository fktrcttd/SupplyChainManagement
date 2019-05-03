using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SCM.Domain.Enums;

namespace SCM.Models
{
    [DisplayName("Категория образцов")]
    public class SampleCategory : Entity
    {
        [DisplayName("Наименование категории")]
        public new string Title { set; get; }
        
        [DisplayName("Образцы")]
        public virtual ICollection<Sample> Samples { get; set; }
        
        [DisplayName("Базовые химические элементы")]
        public virtual ICollection<ChemicalElement> ChemicalElements { get; set; }

        [DisplayName("Область применения образцов")]
        public SampleCategoryType SampleCategoryType { get; set; }
        
        [DisplayName("Виртуальный путь к картинке")]
        public string ImageName { get; set; }
    }
}