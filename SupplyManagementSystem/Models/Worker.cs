using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class Worker : Entity
    {
        [DisplayName("Фамилия")]
        public string FirstName { set; get; }

        [DisplayName("Имя")]
        public string Name { set; get; }
        [DisplayName("Отчество")]

        public string ThirdName { set; get; }

        [DisplayName("Квалификация")]
        public string Qualification { set; get; }

        [DisplayName("Должность")]
        public decimal Position { set; get; }

        [DisplayName("Заработная плата")]
        public decimal Salary { set; get; }
        public virtual ICollection<Release> Releases { get; set; }
    }

}