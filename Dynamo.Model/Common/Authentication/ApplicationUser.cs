using Dynamo.Model.Common.UserProfiles;
using Dynamo.Model.Factories;
using Microsoft.AspNetCore.Identity;

namespace Dynamo.Model.Common.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public int FactoryId { get; set; }

        public Factory Factory { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
