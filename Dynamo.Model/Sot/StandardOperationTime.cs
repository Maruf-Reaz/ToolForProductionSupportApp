using Dynamo.Model.Factories;
using Dynamo.Model.LineBalancing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class StandardOperationTime
    {
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        [Display(Name = "Select Conception")]
        public int SotModelId { get; set; }

        [Display(Name = "CC-Conception Name")]
        public SotModel SotModel { get; set; }

        public List<SectionStandardOperationTime> SectionStandardOperationTimes { get; set; }

        public List<StandardOperationTimeItem> StandardOperationTimeItems { get; set; }

        public int? CalculationStatusId { get; set; }
        public CalculationStatus CalculationStatus { get; set; }

        public int? ValidationStatusId { get; set; }
        public ValidationStatus ValidationStatus { get; set; }

        public List<ParticularLineBalancing> ParticularLineBalancings { get; set; }
    }
}
