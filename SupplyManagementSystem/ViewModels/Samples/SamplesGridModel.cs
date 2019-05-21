using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SCM.Models;

namespace SCM.ViewModels.Samples
{
    public class SamplesGridModel
    {

        public int Id { get; set; }
        
        [Display(Name = "Название")]
        public string Title { get; set; }
        
        [Display(Name = "Количество")]
        [Range(0, 10000, ErrorMessage = "Количество должно быть в диапазоне 0 - 10000")]
        public int Amount { get; set; }
        
        [DisplayName("Ссылка на изображение")]
        public string ImageLink { get; set; }

        [DisplayName("Вес образца")]
        [Range(0, 10000, ErrorMessage = "Вес должен быть в диапазоне 0 - 10000")]
        public double Weight { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Срок годности")]
        public DateTime ExpiryDate { get; set; }

        public string FormatDate => GetFormatDate();

        private string GetFormatDate()
        {
            return ExpiryDate.ToShortDateString();
        }
        public string ChemicalCompositionAsString { get; set; }

        
    }
}