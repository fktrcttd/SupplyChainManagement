using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SCM.Models;

namespace SCM.ViewModels.Order
{
    public class OrderGridModel
    {

        public int Id { get; set; }
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


        public string ClientOrganization { get; set; }
        
        
    }
}