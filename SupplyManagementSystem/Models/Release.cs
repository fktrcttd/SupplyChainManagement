using SCM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class Release : Entity

    {   [ForeignKey("Sample")]
        public int SampleId { set; get; }
        public Sample Sample { set; get; }

        [ForeignKey("Worker")]
        public int WorkerId { set; get; }
        public Worker Worker { set; get; }

        [DisplayName("Дата")]
        public DateTime Date { set; get; }

        [DisplayName("Количество выпущенных образцов")]
        public int Quantity { set; get; }


    }
}