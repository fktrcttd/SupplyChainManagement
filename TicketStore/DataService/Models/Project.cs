using System.Collections.Generic;
using System.ComponentModel;
using TicketStore.Core;

namespace TicketStore.DataService.Models
{
    public class Project : Entity
    {
        [DisplayName("Проектные задачи")]
        public virtual ICollection<Task> Tasks { get; set; }
        
        
        public override void Adding(AppDataContext context)
        {
           
        }
        
        public override void Added(AppDataContext context)
        {
           
        }

        public override void Updating(AppDataContext context)
        {
            
        }

        public override void Updated(AppDataContext context)
        {
            
        }
    }
}