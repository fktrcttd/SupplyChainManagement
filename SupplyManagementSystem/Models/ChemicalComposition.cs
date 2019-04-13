using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    
    /// <summary>
    /// Химический состав. Представляет собой сущность, доступную к заказу. Приобретается образец, состоящий из этого химического состава 
    /// </summary>
    public class ChemicalComposition : Entity
    {
        /// <summary>
        /// Наименование химического состава
        /// </summary>
        [DisplayName("Химический состав")]
        public new string Title { set; get; }

        /// <summary>
        /// Связки химических элементов и составов, в которых участвует данный элемент
        /// </summary>
        public virtual ICollection<CompositionsElement> CompositionsElements { set; get; }
        
        /// <summary>
        /// Ключ категории образцов, к которой привязан химический состав
        /// </summary>
        [ForeignKey("SampleCategory")]
        public int SampleCategoryId { set; get; }
        
        /// <summary>
        /// Объект категории образцов, к которой привязан данный химический состав
        /// </summary>
        public virtual SampleCategory SampleCategory { set; get; }
        
        /// <summary>
        /// Наличие элементов с таким химическим составом на складе
        /// </summary>
        [Display(Name = "Наличие")]
        public int Amount { get; set; }
    }
}