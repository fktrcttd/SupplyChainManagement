using System.Collections.Generic;
namespace SCM.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<Models.SampleCategory> SampleCategories { get; set; }
    }
}