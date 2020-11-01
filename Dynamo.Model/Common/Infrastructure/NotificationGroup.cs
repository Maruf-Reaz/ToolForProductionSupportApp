using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Common.Infrastructure
{
    public class NotificationGroup
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
