using System.ComponentModel.DataAnnotations;

namespace SCM.ViewModels.Samples
{
    public class SampleOrderModel
    {

        public string Title { get; set; }
        public int SampleId { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 10000, ErrorMessage = "От 1 до 10000")]
        public int Amount { get; set; }
        
        public int InStock { get; set; }
    }
}