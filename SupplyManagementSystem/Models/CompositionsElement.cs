using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public class CompositionsElement
    {
        /// <summary>
        /// Ключ химического элемента
        /// </summary>
        [Key, Column(Order = 0)]
        public int ChemicalElementId { get; set; }
        
        /// <summary>
        /// Ключ химического состава
        /// </summary>
        [Key, Column(Order = 1)]
        public int ChemicalCompositionId { get; set; }

        /// <summary>
        /// Объект химического элемента
        /// </summary>
        public virtual ChemicalElement ChemicalElement { get; set; }
        
        /// <summary>
        /// Объект химического состава
        /// </summary>
        public virtual ChemicalComposition ChemicalComposition { get; set; }

        /// <summary>
        /// Процент содержания химического элемента
        /// </summary>
        [Range(0,100, ErrorMessage = "Процент должен быть числом от 0 до 100")]
        public decimal Percentage { get; set; }
    }
}