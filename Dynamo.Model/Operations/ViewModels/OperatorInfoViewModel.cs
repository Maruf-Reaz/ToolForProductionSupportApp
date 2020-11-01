using Dynamo.Model.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Operations.ViewModels
{
    public class OperatorInfoViewModel
    {
        public string LineNumber { get; set; }

        public string SectionName { get; set; }

        public string Name { get; set; }

        public string IdCardNumber { get; set; }
        public List<OperatorInfoSkillMatrixViewModel> OperatorInfoSkillMatrixViewModels { get; set; }
    }
}
