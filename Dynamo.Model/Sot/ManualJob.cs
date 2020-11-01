using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class ManualJob
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        [Required(ErrorMessage = "Please Enter the Name of Manual Job")]
        public string Name { get; set; }

        public List<ManualJobStandardOperationTimeItem> ManualJobStandardOperationTimeItems { get; set; }
    }
}
