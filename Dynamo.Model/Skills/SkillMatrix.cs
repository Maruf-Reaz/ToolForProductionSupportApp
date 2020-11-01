using Dynamo.Model.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dynamo.Model.Skills
{
    public class SkillMatrix
    {
        public int Id { get; set; }

        public int OperationId { get; set; }
        public Operation Operation { get; set; }

        public int OperatorId { get; set; }
        public Operator Operator { get; set; }

        [Display(Name = "SOT In Second")]
        public double StandardSotInSecond { get; set; }

        [Display(Name = "RFT")]
        public double StandardRft { get; set; }

        [Display(Name = "SOT Score")]
        public double SotScore { get; set; }

        [Display(Name = "RFT Score")]
        public double RftScore { get; set; }

        [Required]
        [Display(Name = "Target Month")]
        public double? TargetMonth { get; set; }

        [Required]
        [Display(Name = "Target Grade")]
        public string TargetGrade { get; set; }

        [Display(Name = "Updated On")]
        public DateTime UpdatedOn { get; set; }

        [NotMapped]
        public string LineLineNumber { get; set; }

        [NotMapped]
        public string SectionName { get; set; }

        [NotMapped]
        public string OperatorName { get; set; }

        [NotMapped]
        public string IdCardNumber { get; set; }

        [NotMapped]
        public DateTime JoiningDate { get; set; }

        [NotMapped]
        public string Address { get; set; }

        [NotMapped]
        public string PhoneNumber { get; set; }
    }
}
