using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.CICalendar.ViewModels
{
    public class CalendarEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Description { get; set; }
        public bool AllDay { get; set; }
        public string StatusColour { get; set; }
        public List<string> GuestUserId { get; set; }
    }
}
