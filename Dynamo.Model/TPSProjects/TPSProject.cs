using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.TPSProjects
{
    public class TPSProject
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        [Required(ErrorMessage = "Please Enter a Description")]
        public string Description { get; set; }

        [Display(Name = "Date of Upload")]
        public DateTime DateOfUpload { get; set; }

        [Display(Name = "Attachment 1")]
        public string ProjectFileName1 { get; set; }

        [Display(Name = "Attachment 2")]
        public string ProjectFileName2 { get; set; }
    }
}
