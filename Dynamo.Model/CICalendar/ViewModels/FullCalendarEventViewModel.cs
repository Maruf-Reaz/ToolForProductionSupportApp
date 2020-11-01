using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.CICalendar.ViewModels
{
    public class FullCalendarEventViewModel
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public bool editable { get; set; }
        public bool disabled { get; set; }
        public string className { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
