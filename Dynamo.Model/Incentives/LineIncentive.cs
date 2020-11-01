using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Incentives
{
    public class LineIncentive
    {
        public int Id { get; set; }

        public int LineId { get; set; }
        public Line Line { get; set; }

        public DateTime LineIncentiveDateTime { get; set; }
                
        public double? Total { get; set; }

        public bool Enable { get; set; }

        public string Comments { get; set; }
    }
}
