using Dynamo.Model.Incentives;
using Dynamo.Model.LineBalancing;
using Dynamo.Model.Operations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dynamo.Model.Factories
{
    public class Line
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Section")]
        public int? SectionId { get; set; }
        public Section Section { get; set; }

        //public LineIncentive LineIncentive { get; set; }
        public List<LineIncentive> LineIncentives { get; set; }

        [Required(ErrorMessage = "Please Enter the Line Number")]
        [Display(Name = "Line Number")]
        public string LineNumber { get; set; }

        public ParticularLineBalancing ParticularLineBalancings { get; set; }

        public List<Operator> Operators { get; set; }

        [NotMapped]
        public string SectionName { get; set; }

        [NotMapped]
        public double? LineIncentiveTotal { get; set; }
    }
}
