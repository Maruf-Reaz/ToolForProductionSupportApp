using Dynamo.Model.Factories;
using Dynamo.Model.Operations;
using Dynamo.Model.Sot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Machines
{
    public class Machine
    {
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        [Required(ErrorMessage = "Please Enter the Name of Machine")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter a Machine Code")]
        public string Code { get; set; }

        public double? Physiology { get; set; }

        [Display(Name = "Basic Tiredness")]
        public double? BasicTiredness { get; set; }

        [Display(Name = "Noisey Environment")]
        public double? NoiseyEnvironment { get; set; }

        [Display(Name = "Change Bobin")]
        public double? ChangeBobin { get; set; }

        [Display(Name = "Stand Working")]
        public double? StandWorking { get; set; }

        public double? Other { get; set; }

        [Display(Name = "Field 1")]
        public double? Field1 { get; set; }

        [Display(Name = "Field 2")]
        public double? Field2 { get; set; }

        [Display(Name = "Field 3")]
        public double? Field3 { get; set; }

        public double? Total { get; set; }

        public List<Operation> Operations { get; set; }
    }
}
