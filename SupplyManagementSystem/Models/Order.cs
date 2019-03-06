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

        [DisplayName("Количество образцов в заказе")]
        public int QuantitySample { set; get; }

        [DisplayName("Статус заказа")]
        public bool IsFinished { set; get; }

        public ICollection<Sample> Samples { get; set; }
    }
}