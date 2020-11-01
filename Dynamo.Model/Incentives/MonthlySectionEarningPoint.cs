using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Incentives
{
    public class MonthlySectionEarningPoint
    {
        public int Id { get; set; }

        [Required]
        public int SectionId { get; set; }
        public Section Section { get; set; }

        [Required]
        [Display(Name = "Target Point")]
        public double TargetPoint { get; set; }

        [Required]
        [Display(Name = "Money Per Point")]
        public double MoneyPerPoint { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

    }
}
