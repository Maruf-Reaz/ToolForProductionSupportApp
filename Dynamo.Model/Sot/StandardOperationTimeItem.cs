using Dynamo.Model.Factories;
using Dynamo.Model.Machines;
using Dynamo.Model.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class StandardOperationTimeItem
    {
        public int Id { get; set; }

        public int? SerialNumber { get; set; }

        public int StandardOperationTimeId { get; set; }
        public StandardOperationTime StandardOperationTime { get; set; }

        public int DataSourceId { get; set; }
        public DataSource DataSource { get; set; }

        public int OperationId { get; set; }
        public Operation Operation { get; set; }

        public string OperationCode { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public string VideoLink { get; set; }

        public double? CycleTime { get; set; }

        public double? Size { get; set; }

        public double? Rating { get; set; }

        public double? Cycle { get; set; }

        public double? SotValue { get; set; }

        public List<StandardOperationTimeSubItem> StandardOperationTimeSubItems { get; set; }

        public List<ManualJobStandardOperationTimeItem> ManualJobStandardOperationTimeItems { get; set; }
    }
}
