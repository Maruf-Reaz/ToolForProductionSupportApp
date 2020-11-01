using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Skills
{
    public class SkillMatrixRange
    {
        public int Id { get; set; }
        

        public int Code { get; set; }
      
        public string Level { get; set; }

        public int FactoryId { get; set; }
        public Factory Factory { get; set; }
    }
}
