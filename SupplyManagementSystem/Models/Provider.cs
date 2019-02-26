using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class Provider: Entity
    {  
        [DisplayName ("Наименование организации")]
        public new string Title { set; get; }

        public string Inn { set; get; }

    }
}