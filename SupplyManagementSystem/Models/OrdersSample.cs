using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SCM.Core;

namespace SCM.Models
{
    public class OrdersSample
    {
        /// <summary>
        /// Ключ заказа
        /// </summary>
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }
        
        /// <summary>
        /// Ключ образца
        /// </summary>
        [Key, Column(Order = 1)]
        public int SampleId { get; set; }

        /// <summary>
        /// Объект заказа
        /// </summary>
        public virtual Order Order { get; set; }
        
        /// <summary>
        /// Объект образца
        /// </summary>
        public virtual Sample Sample { get; set; }
    }
}