using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class ValidationStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<StandardOperationTime> StandardOperationTimes { get; set; }
    }
}
