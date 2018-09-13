using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TicketStore.Core;
using TicketStore.Models;

namespace TicketStore.DataService.Models
{
    public class Project : Entity
    {
        [DisplayName("Созданные задачи")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}