using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class DataSource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Calculation File")]
        public string CalculationFileName { get; set; }

        public List<StandardOperationTimeItem> StandardOperationTimeItems { get; set; }
    }
}
