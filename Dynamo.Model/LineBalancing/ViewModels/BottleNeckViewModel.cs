using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.LineBalancing.ViewModels
{
    public class BottleNeckViewModel
    {
        public string FirstBottleNeckName { get; set; }

        public double? FirstBottleNeckValue { get; set; }

        public string SecondBottleNeckName { get; set; }

        public double? SecondBottleNeckValue { get; set; }
    }
}
