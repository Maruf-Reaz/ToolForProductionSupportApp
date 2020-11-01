using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Skills.ViewModels
{
    public class SkillMatrixViewModel
    {
        public int rowId { get; set; }
        public int operationId { get; set; }
        public int operatorId { get; set; }
        public double standardSot { get; set; }
        public double standardRft { get; set; }
        public double scoreSot { get; set; }
        public double scoreRft { get; set; }
        public double? targetMonth { get; set; }
        public string targetGrade { get; set; }
    }
}
