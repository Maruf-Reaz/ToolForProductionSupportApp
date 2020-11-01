using Dynamo.Model.Common.Authentication;
using Dynamo.Model.ELearnings;
using Dynamo.Model.Machines;
using Dynamo.Model.Operations;
using Dynamo.Model.Skills;
using Dynamo.Model.Sot;
using Dynamo.Model.TPSProjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Factories
{
    public class Factory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Supplier Code")]
        public string PhoneNumber { get; set; }

        public List<Section> Sections { get; set; }

        public List<TPSProject> TPSProjects { get; set; }

        public List<ELearning> ELearnings { get; set; }

        public List<ManualJob> ManualJobs { get; set; }

        public List<SotModel> SotModels { get; set; }

        public List<SignSport> SignSports { get; set; }

        public List<Process> Processes { get; set; }

        public List<Machine> Machines { get; set; }

        public List<Operation> Operations { get; set; }

        public List<StandardOperationTime> StandardOperationTimes { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }

        public List<SkillMatrixRange> SkillMatrixRanges { get; set; }
    }
}
