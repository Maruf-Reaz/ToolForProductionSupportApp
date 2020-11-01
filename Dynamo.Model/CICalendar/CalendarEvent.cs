using Dynamo.Model.Common.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.CICalendar
{
    public class CalendarEvent
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Description { get; set; }

        public string StatusColour { get; set; }

        public List<EventGuest> EventGuests { get; set; }

        public string CreatedByUserId { get; set; }

        public bool AllDay { get; set; }
    }
}
