using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SCM.ViewModels.Samples;

namespace SCM.ViewModels.Order
{
    public class FullOrderViewModel
    {

        [DisplayName("Клиент")]
        public string ClientTitle { get; set; }
        public int ClientId { get; set; }
        public List<SampleOrderModel> Samples { get; set; }
        
        [DisplayName("Наименование заказа")]
        public string Title { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата оформления заказа")]
        public DateTime CreationDateTime { get; set; }

        
    }
}