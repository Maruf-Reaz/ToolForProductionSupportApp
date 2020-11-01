using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Common.Infrastructure
{
    public class Chat
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string SentBy { get; set; }
        public string SentByUserName { get; set; }

        public string SentTo { get; set; }
        public string SentToUserName { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }


    }
}
