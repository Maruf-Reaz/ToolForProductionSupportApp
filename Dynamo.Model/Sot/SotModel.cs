using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class SotModel
    {
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        [Display(Name = "Conception Name")]
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

        public string Photo { get; set; }

        public List<StandardOperationTime> StandardOperationTimes { get; set; }

        [NotMapped]
        public string ProcessName { get; set; }

        [NotMapped]
        public string SignSportName { get; set; }
    }
}
