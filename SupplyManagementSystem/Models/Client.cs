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
        /// ФИО клиента
        /// </summary>
        [DisplayName("ФИО")]
        public string Name { set; get; }

        /// <summary>
        /// Органиация
        /// </summary>
        [DisplayName("Организация")]
        public string Organization { set; get; }
        
        /// <summary>
        /// ИНН
        /// </summary>
        [DisplayName("ИНН")]
        public string Inn { set; get; }
        
        /// <summary>
        /// Расчетный счет
        /// </summary>
        [DisplayName("Расчетный счет")]
        public string Check { set; get; }
        
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