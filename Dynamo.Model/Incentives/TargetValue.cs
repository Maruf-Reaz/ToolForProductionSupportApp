using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Incentives
{
    public class TargetValue
    {
        public int Id { get; set; }

        public int LineIncentiveId { get; set; }
        public LineIncentive LineIncentive { get; set; }

        public int IncentiveVariableId { get; set; }

        public double Value { get; set; }
    }
}
