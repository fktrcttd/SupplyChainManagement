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
        /// <summary>
        /// Наименование химического элементы
        /// </summary>
        [DisplayName("'Элемент")]
        public new string Title { set; get; }

        /// <summary>
        /// обозначение химического элемента. Например, Si - кремний
        /// </summary>
        [DisplayName("Обозначение")]
        public  string Symbol { set; get; }
        
        /// <summary>
        /// Связки химических элементов и составов, в которых участвует данный элемент
        /// </summary>
        public virtual ICollection<CompositionsElement> CompositionsElements { set; get; }
        
        /// <summary>
        /// Категории образцов, в которых участвует данный хим. элемент
        /// </summary>
        public virtual ICollection<SampleCategory> SampleCategories { get; set; }
    }
}