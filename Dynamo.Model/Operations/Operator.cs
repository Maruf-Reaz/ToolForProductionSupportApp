using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dynamo.Model.Operations
{
    public class Operator
    {
        public int Id { get; set; }

        [Display(Name = "Line Number")]
        public int? LineId { get; set; }
        public Line Line { get; set; }

        [Required]
        [Display(Name = "Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Id Card Number")]
        public string IdCardNumber { get; set; }

        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public string LineNumber { get; set; }

        [NotMapped]
        public string SectionName { get; set; }
    }
}
