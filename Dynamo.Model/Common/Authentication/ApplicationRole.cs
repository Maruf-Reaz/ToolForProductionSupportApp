using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dynamo.Model.Common.Authentication
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }
    }
}
