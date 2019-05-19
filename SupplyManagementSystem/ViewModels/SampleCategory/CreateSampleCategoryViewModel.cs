using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace SCM.ViewModels.SampleCategory
{
    public class CreateSampleCategoryViewModel
    {
        /// <summary>
        /// Наименование категории
        /// </summary>
        [DisplayName("Наименование категории")]
        public string Title { set; get; }

        /// <summary>
        /// Перечень базовых химических элементов
        /// </summary>
        [DisplayName("Перечень базовых химических элементов")]
        public int[] BaseChemicalElementId { get; set; }
        
        //To change label title value  
        [DisplayName("Ссылка на изображение")]  
        public string ImageLink { get; set; }  

    }
}