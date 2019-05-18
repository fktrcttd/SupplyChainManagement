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
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        [DisplayName("Фамилия")]
        public string Name { set; get; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        [DisplayName("Имя")]
        public string FirstName { set; get; }

        /// <summary>
        /// Отчество клиента
        /// </summary>
        [DisplayName("Отчество")]
        public string ThirdName { set; get; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [DisplayName("E-mail")]
        public string Address { set; get; }

        [DisplayName("Номер телефона")]
        public string Phone { set; get; }
        
        public virtual ICollection<Order> Orders { set; get; }
    }
}