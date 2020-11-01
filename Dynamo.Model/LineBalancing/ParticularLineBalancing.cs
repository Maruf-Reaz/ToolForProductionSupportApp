using Dynamo.Model.Factories;
using Dynamo.Model.Sot;

namespace Dynamo.Model.LineBalancing
{
    public class ParticularLineBalancing
    {
        public int Id { get; set; }

        public int LineId { get; set; }
        public Line Line { get; set; }

        public int StandardOperationTimeId { get; set; }
        public StandardOperationTime StandardOperationTime { get; set; }

        public double ActualLineBalancingRatio { get; set; }

        public string Who { get; set; }
    }

}
