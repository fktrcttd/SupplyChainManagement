using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class Fuel:Entity
    {
        [DisplayName("Наименование топлива")]
        public new string Title { set; get; }
        [DisplayName("Теплотворная способность")]
        public string ColorificValue { set; get; }
        public ICollection<Supply> Supplies { get; set; } //мыслить абстракциями
    }
}