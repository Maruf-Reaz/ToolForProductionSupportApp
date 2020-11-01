using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Sot.ViewModels
{
    public class SotModelViewModel
    {
        public int Id { get; set; }

        public int FactoryId { get; set; }

        [Required(ErrorMessage = "Please Enter Name of the Model")]
        public string Name { get; set; }

        [Display(Name = "DIPL")]
        public string Dipl { get; set; }

        [Display(Name = "Select Process")]
        public int ProcessId { get; set; }
        public Process Process { get; set; }

        [Display(Name = "Select Sign Sport")]
        public int SignSportId { get; set; }
        [Display(Name = "Sign Sport")]
        public SignSport SignSport { get; set; }

        public IFormFile Photo { get; set; }

        public string OldPhoto { get; set; }
    }
}
