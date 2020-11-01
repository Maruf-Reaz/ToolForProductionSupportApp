using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Common.Infrastructure
{
    public class UserChatViewModel
    {      
        public int UnReadChatNumber { get; set; }

        public string LastMessage { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string PhotoName { get; set; }

        public int Status { get; set; }

        public DateTime? LastChatTime { get; set; }
    }
}
