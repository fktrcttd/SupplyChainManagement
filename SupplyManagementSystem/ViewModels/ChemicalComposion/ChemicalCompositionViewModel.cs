using System.Collections.Generic;

namespace SCM.ViewModels.ChemicalComposion
{
    public class ChemicalCompositionViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<ChemicalElementViewModel> Elements { get; set; }
    }
}