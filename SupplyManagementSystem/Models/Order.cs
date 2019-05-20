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
    public class Order : Entity
    {
        [DisplayName("Наименование заказа")]
        public new string Title { set; get; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата оформления заказа")]
        public DateTime Date { set; get; }


        [NotMapped]
        public string FormatDate => GetDate();

        private string GetDate()
        {
            return Date.ToShortDateString();
        }

        [DisplayName("Статус заказа")]
        public bool IsFinished { set; get; }

        [NotMapped]
        public string IsFinishedAsString => GetStatus();

        private string GetStatus()
        {
            return IsFinished ? "Завершен": "Не завершен";
        }

        /// <summary>
        /// Образцы в заказе
        /// </summary>
        public ICollection<OrdersSample> OrdersSamples { get; set; }
        
        [DisplayName("Организация заказщика")]
        public string OrganizationClient { set; get; }
        
        
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
    }
}