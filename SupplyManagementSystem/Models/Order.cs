using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        [DisplayName("Статус заказа")]
        public bool IsFinished { set; get; }

        /// <summary>
        /// Образцы в заказе
        /// </summary>
        public ICollection<OrdersSample> OrdersSamples { get; set; }
        
        [DisplayName("Количество образцов в заказе")]
        public int QuantitySample { set; get; }
    }
}