using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.ELearnings.ViewModels
{
    public class ELearningViewModel
    {
        public int Id { get; set; }

        public int FactoryId { get; set; }

        [Required(ErrorMessage = "Please Enter a Description")]
        public string Description { get; set; }

        [Display(Name = "Date of Upload")]
        public DateTime DateOfUpload { get; set; }

        [Display(Name = "Attachment 1")]
        public IFormFile ELearningFile1 { get; set; }

        [Display(Name = "Attachment 2")]
        public IFormFile ELearningFile2 { get; set; }

        [Display(Name = "Attachment 1")]
        public string OldELearningFileName1 { get; set; }

        [Display(Name = "Attachment 2")]
        public string OldELearningFileName2 { get; set; }
    }
}
