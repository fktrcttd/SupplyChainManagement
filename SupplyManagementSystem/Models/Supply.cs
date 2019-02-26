using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class Supply:Entity
    {
       
        [DisplayName("Наименование поставки")]
        public new string Title { set; get; }

        public DateTime Date { set; get; }

        public ICollection<Fuel> Fuels { get; set; } //мыслить абстракциями

    }
}