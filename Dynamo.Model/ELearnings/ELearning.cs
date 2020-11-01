using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.ELearnings
{
    public class ELearning
    {
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        [Required(ErrorMessage = "Please Enter a Description")]
        public string Description { get; set; }

        [Display(Name = "Date of Upload")]
        public DateTime DateOfUpload { get; set; }

        [Display(Name = "Attachment 1")]
        public string ELearningFileName1 { get; set; }

        [Display(Name = "Attachment 2")]
        public string ELearningFileName2 { get; set; }
    }
}
