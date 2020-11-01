using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class ManualJobStandardOperationTimeItem
    {
        public int StandardOperationTimeItemId { get; set; }

        public StandardOperationTimeItem StandardOperationTimeItem { get; set; }

        public int ManualJobId { get; set; }

        public ManualJob ManualJob { get; set; }

        public double Time { get; set; }

        public int Quantity { get; set; }
    }
}
