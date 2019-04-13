using System.Collections.Generic;
using SCM.Models;

namespace SCM.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<SampleCategory> SampleCategories { get; set; }
    }
}