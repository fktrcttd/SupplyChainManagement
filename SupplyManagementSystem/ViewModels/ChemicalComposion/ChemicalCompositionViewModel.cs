using System.Collections.Generic;

namespace SCM.ViewModels.ChemicalComposion
{
    public class ChemicalCompositionViewModel
    {
        public string Name { get; set; }
        public List<ChemicalElementViewModel> Elements { get; set; }
    }
}