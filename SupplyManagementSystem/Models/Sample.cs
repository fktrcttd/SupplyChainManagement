using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SCM.Core;

namespace SCM.Models
{
    public class Sample : Entity
    {
        [Display(Name = "Название")]
        public new string Title { get; set; }
        /// <summary>
        /// Объект химического состава, из которого состоит образец
        /// </summary>
        public virtual ChemicalComposition ChemicalComposition { get; set; }

        /// <summary>
        /// Вторичный ключ для связи с химическим составом. Не может отсутствовать
        /// </summary>
        [ForeignKey(nameof(ChemicalComposition))]
        public int ChemicalCompositionId { get; set; }

        /// <summary>
        /// Заказы, в которых участвовал данный образец
        /// </summary>
        public virtual ICollection<OrdersSample> OrdersSamples { get; set; }
        
        /// <summary>
        /// Количество образца на складе
        /// </summary>
        [Display(Name = "Количество")]
        public int Amount { get; set; }
        
        [DisplayName("Ссылка на изображение")]
        public string ImageLink { get; set; }

        [DisplayName("Вес образца")]
        public double Weight { get; set; }

        [DisplayName("Срок годности")]
        public DateTime DateTime { get; set; }
        
        
        
    }
}