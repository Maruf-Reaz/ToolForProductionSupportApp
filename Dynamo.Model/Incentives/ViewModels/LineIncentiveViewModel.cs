using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Incentives.ViewModels
{
    public class LineIncentiveViewModel
    {
        public string DateString { get; set; }

        public LineIncentive LineIncentive { get; set; }

        public List<LineIncentiveVariableValueViewModel> TargetValues { get; set; }

        public List<LineIncentiveVariableValueViewModel> AchievedValues { get; set; }

        public List<LineIncentiveVariableValueViewModel> PointValues { get; set; }
    }
}
