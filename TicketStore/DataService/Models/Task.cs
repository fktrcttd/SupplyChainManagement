using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using TicketStore.Core;
using TicketStore.Models;

namespace TicketStore.DataService.Models
{
    public class Task: Entity
    {
        
        [ForeignKey("Project")]
        [DataType("ForeignKey")]
        public int? ProjectId { get; set; }

        [DataType("Reference")]
        public virtual Project Project { get; set; }

        
        [DisplayName("Оценка")]
        public TimeSpan EstimationTimeSpan { get; set; }
        
        [DisplayName("Затрачено")]
        public TimeSpan ElapsedTimeSpan { get; set; }
        
        [DisplayName("Затрачено")]
        public TaskStatus TaskStatus { get; set; }
        
        
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