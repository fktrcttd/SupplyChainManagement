using System.ComponentModel;

namespace SCM.ViewModels.Clients
{
    public class ClientGridModel
    {

        public int Id { get; set; }
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
        public string Email { set; get; }

        [DisplayName("Номер телефона")] public string Phone { set; get; }
        
    }
}