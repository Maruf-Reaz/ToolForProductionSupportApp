using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Common.Infrastructure
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NotificationGroup> NotificationGroups { get; set; }

    }
}
