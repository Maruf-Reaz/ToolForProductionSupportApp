using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Common.Infrastructure
{
    public class NotificationApplicationUser
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public string UserId { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
