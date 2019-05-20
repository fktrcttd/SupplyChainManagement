using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCM.ViewModels.Clients
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        public string Phone { get; set; }
        
        public string Comment { get; set; }
        
        
        [DisplayName("Согласие на обработку персональных данных")]
        [Required(ErrorMessage = "Обязательное поле")]
        public bool PersonalDataAgreement { get; set; }
    }
}