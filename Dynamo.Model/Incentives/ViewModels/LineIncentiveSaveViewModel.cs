using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Incentives.ViewModels
{
    public class LineIncentiveSaveViewModel
    {
        public int rowId { get; set; }

        public int LineId { get; set; }

        public string Date { get; set; }

        public double? Total { get; set; }

        public bool Enable { get; set; }

        public string Comments { get; set; }

        public List<LineIncentiveVariableValueViewModel> TargetValues { get; set; }

        public List<LineIncentiveVariableValueViewModel> AchievedValues { get; set; }

        public List<LineIncentiveVariableValueViewModel> PointValues { get; set; }
    }
}
