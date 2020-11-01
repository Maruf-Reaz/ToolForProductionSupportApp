using Dynamo.Model.Factories;
using Dynamo.Model.Machines;
using Dynamo.Model.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.LineBalancing
{
    public class LineBalancingState
    {
        public int Id { get; set; }

        public int ParticularLineBalancingId { get; set; }
        public ParticularLineBalancing ParticularLineBalancing { get; set; }

        public string OperationName { get; set; }

        public int OperationId { get; set; }
        public Operation Operation { get; set; }

        public int MachineId { get; set; }
        public Machine Machine { get; set; }

        public int? OperatorId1 { get; set; }
        public int? OperatorId2 { get; set; }
        public int? OperatorId3 { get; set; }
        public int? OperatorId4 { get; set; }

        public int? OperatorNo1 { get; set; }
        public int? OperatorNo2 { get; set; }
        public int? OperatorNo3 { get; set; }
        public int? OperatorNo4 { get; set; }

        public double? CycleTime1 { get; set; }
        public double? CycleTime2 { get; set; }
        public double? CycleTime3 { get; set; }
        public double? CycleTime4 { get; set; }

        public double? AllocatedTime1 { get; set; }
        public double? AllocatedTime2 { get; set; }
        public double? AllocatedTime3 { get; set; }
        public double? AllocatedTime4 { get; set; }

        public double? Output1 { get; set; }
        public double? Output2 { get; set; }
        public double? Output3 { get; set; }
        public double? Output4 { get; set; }

    }
}
