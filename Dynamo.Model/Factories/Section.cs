using Dynamo.Model.Operations;
using Dynamo.Model.Sot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dynamo.Model.Factories
{
    public class Section
    {
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        [Required(ErrorMessage = "Please Enter a Name for the Section")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter a Short Name for the Section")]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "Process")]
        public int ProcessId { get; set; }
        public Process Process { get; set; }

        public List<Operator> Operators { get; set; }

        public List<Line> Lines { get; set; }

        public List<Operation> Operations { get; set; }

        public List<StandardOperationTimeItem> StandardOperationTimeItems { get; set; }

        public List<SectionStandardOperationTime> SectionStandardOperationTimes { get; set; }

        [NotMapped]
        public string ProcessName { get; set; }
    }
}
