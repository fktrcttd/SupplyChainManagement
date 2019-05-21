using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCM.ViewModels.Order
{
    public class SimpleOrderViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public int ClientId { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Образцы")]
        public int[] SampleId { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Наименование заказа")]
        public string Title { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата оформления заказа")]
        
        public DateTime CreationDateTime { get; set; }
        
    }
}