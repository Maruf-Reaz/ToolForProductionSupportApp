using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.CICalendar
{
    public class EventGuest
    {
        public int Id { get; set; }

        public int CalendarEventId { get; set; }
        //public CalendarEvent CalendarEvent { get; set; }

        public string UserId { get; set; }

        public int Status { get; set; } //1 means pending //2 means accepted // 3 means rejected
    }
}
