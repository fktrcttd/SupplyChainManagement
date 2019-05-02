using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SCM.ViewModels
{
    public class ClientViewModel
    {

        [DisplayName("Фамилия")]
        public string Name { set; get; }

        [DisplayName("Имя")]
        public string FirstName { set; get; }

        [DisplayName("Отчество")]
        public string ThirdName { set; get; }

        [DisplayName("Адрес")]
        public string Address { set; get; }

        [DisplayName("Номер телефона")]
        public string Phone { set; get; }

    }
}