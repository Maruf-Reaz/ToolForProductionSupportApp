using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class SectionStandardOperationTime
    {
        public int StandardOperationTimeId { get; set; }

        public StandardOperationTime StandardOperationTime { get; set; }

        public int SectionId { get; set; }

        public Section Section { get; set; }

        public double SotValue { get; set; }
    }
}
