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
        
        
        [DisplayName("Базовые химические элементы")]
        public virtual ICollection<ChemicalElement> ChemicalElements { get; set; }

        [DisplayName("Область применения образцов")]
        public SampleCategoryType SampleCategoryType { get; set; }
        
        [DisplayName("Ссылка на картинку")]
        public string ImageLink { get; set; }
    }
}