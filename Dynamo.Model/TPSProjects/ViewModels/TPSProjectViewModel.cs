using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.TPSProjects.ViewModels
{
    public class TPSProjectViewModel
    {
        public int Id { get; set; }

        public int FactoryId { get; set; }

        [Required(ErrorMessage = "Please Enter a Description")]
        public string Description { get; set; }

        [Display(Name = "Date of Upload")]
        public DateTime DateOfUpload { get; set; }

        [Display(Name = "Attachment 1")]
        public IFormFile ProjectFile1 { get; set; }

        [Display(Name = "Attachment 2")]
        public IFormFile ProjectFile2 { get; set; }

        [Display(Name = "Attachment 1")]
        public string OldProjectFileName1 { get; set; }

        [Display(Name = "Attachment 2")]
        public string OldProjectFileName2 { get; set; }
    }
}
