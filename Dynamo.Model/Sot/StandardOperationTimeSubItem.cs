using Dynamo.Model.Machines;
using Dynamo.Model.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class StandardOperationTimeSubItem
    {
        [Key]
        public int Id { get; set; }

        public int StandardOperationTimeItemId { get; set; }
        public StandardOperationTimeItem StandardOperationTimeItem { get; set; }

        public bool IsNeglected { get; set; }

        public double? CycleStartTime { get; set; }
        public double? CycleEndTime { get; set; }

        public double? Wastage1StartTime { get; set; }
        public double? Wastage1EndTime { get; set; }

        public double? Wastage2StartTime { get; set; }
        public double? Wastage2EndTime { get; set; }

        public double? Wastage3StartTime { get; set; }
        public double? Wastage3EndTime { get; set; }
    }
}
