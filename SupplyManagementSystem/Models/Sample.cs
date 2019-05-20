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
        [DisplayName("Химический состав")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int ChemicalCompositionId { get; set; }
        
        

        /// <summary>
        /// Заказы, в которых участвовал данный образец
        /// </summary>
        public virtual ICollection<OrdersSample> OrdersSamples { get; set; }
        
        /// <summary>
        /// Количество образца на складе
        /// </summary>
        [Display(Name = "Количество")]
        [Range(0, 10000, ErrorMessage = "Количество должно быть в диапазоне 0 - 10000")]
        public int Amount { get; set; }
        
        [DisplayName("Ссылка на изображение")]
        public string ImageLink { get; set; }

        [DisplayName("Вес образца")]
        [Range(0, 10000, ErrorMessage = "Вес должен быть в диапазоне 0 - 10000")]
        public double Weight { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Срок годности")]
        public DateTime ExpiryDate { get; set; }

        [NotMapped]
        public string FormatDate => GetFormatDate();

        private string GetFormatDate()
        {
            return ExpiryDate.ToShortDateString();
        }

        [NotMapped]
        public string ChemicalCompositionAsString => FormatChemicalComposition();

        private string FormatChemicalComposition()
        {
            return this.ChemicalComposition.Title;
        }
    }
}