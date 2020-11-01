using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Incentives
{
    public class MonthlyVariableValue
    {

        public int Id { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public int IncentiveVariableId { get; set; }
        public IncentiveVariable IncentiveVariable { get; set; }

        public double Value { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

    }
}
