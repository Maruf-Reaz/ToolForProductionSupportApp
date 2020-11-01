using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.LineBalancing.ViewModels
{
    public class SearchOperationByLineIdViewModel
    {
        public int? OperationId { get; set; }

        public List<int> OperatorId { get; set; }
    }
}
