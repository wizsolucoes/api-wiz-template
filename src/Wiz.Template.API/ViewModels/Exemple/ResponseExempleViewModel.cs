using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wiz.Template.API.ViewModels.Exemple
{
    public class ResponseExempleViewModel
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}