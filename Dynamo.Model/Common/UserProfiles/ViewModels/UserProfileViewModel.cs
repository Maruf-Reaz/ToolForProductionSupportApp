using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dynamo.Model.Common.UserProfiles.ViewModels
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Nationality { get; set; }

        public IFormFile Photo { get; set; }

        public string OldPhotoName { get; set; }
    }
}
