using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Common.Infrastructure
{
    public class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string AdderId { get; set; }
        public string LinkAction { get; set; }
        public string LinkController { get; set; }
        public DateTime PostingTime { get; set; }
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
        public List<NotificationApplicationUser> NotificationApplicationUsers { get; set; }
    }
}
