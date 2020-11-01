using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Common.Infrastructure
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        public string NotificationText { get; set; }
        public string NotificationLink { get; set; }
        public string AdderId { get; set; }
        public string PostingTime { get; set; }
        public string AdderName { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
