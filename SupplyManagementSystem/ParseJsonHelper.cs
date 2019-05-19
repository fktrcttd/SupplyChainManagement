using Newtonsoft.Json;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SCM
{
    public static class ParseJsonHelper
    {
        public static List<ChemicalElementInJson> GetChemicalElements(string path)
        {
            String value = File.ReadAllText(path);
            var parsed = JsonConvert.DeserializeObject<List<ChemicalElementInJson>>(value);
            return parsed;

        }
    }
}