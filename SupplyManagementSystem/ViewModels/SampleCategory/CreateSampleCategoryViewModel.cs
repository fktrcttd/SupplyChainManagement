using System.Collections.Generic;
using System.ComponentModel;

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

    }
}