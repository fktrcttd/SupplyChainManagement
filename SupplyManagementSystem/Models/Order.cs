using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class Order : Entity
    {
        [DisplayName("Наименование заказа")]
        public new string Title { set; get; }

        [DisplayName("Дата заказа")]
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
    }
}