using Dynamo.Model.Factories;
using Dynamo.Model.Machines;
using Dynamo.Model.Sot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dynamo.Model.Operations
{
    public class Operation
    {
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        [Required]
        [Display(Name = "Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }

        [Required]
        [Display(Name = "Machine")]
        public int MachineId { get; set; }
        public Machine Machine { get; set; }

        [Required]
        public string Name { get; set; }

        public List<StandardOperationTimeItem> StandardOperationTimeItems { get; set; }

        [NotMapped]
        public string SectionName { get; set; }

        [NotMapped]
        public string MachineName { get; set; }
    }
}
