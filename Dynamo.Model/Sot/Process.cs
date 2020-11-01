using Dynamo.Model.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Sot
{
    public class Process
    {
        public int Id { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public List<Section> Sections { get; set; }

        public List<SotModel> SotModels { get; set; }
    }
}
