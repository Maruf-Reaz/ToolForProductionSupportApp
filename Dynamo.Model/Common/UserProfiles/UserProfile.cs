using Dynamo.Model.Common.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Common.UserProfiles
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Nationality { get; set; }

        public string PhotoName { get; set; }
    }
}
