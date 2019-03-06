using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class Client : Entity
    {
        [DisplayName("ФИО")]
        public string Name { set; get; }

        [DisplayName("Адрес")]
        public string Address { set; get; }

        [DisplayName("Номер телефона")]
        public string Phone { set; get; }
        public virtual ICollection<Order> Orders { set; get; }
    }
}