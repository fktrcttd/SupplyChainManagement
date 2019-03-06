using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class Sample : Entity
    {
        [DisplayName("Наименование образца")]
        public new string Title { set; get; }

        [DisplayName("Дата производства")]
        public DateTime Date { set; get; }

        [DisplayName("Цена")]
        public decimal Price { set; get; }

        [DisplayName("Вес образца")]
        public int Weight { set; get; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Release> Releases { get; set; }
        
        public ChemicalComposition ChemicalComposition { get; set; }
        [ForeignKey ("ChemicalComposition")]
        public int ChemicalCompositionId { get; set; }
    }
}