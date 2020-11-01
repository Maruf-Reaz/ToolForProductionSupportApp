using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamo.Data;
using Dynamo.Model.CICalendar;
using Dynamo.Model.CICalendar.ViewModels;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Common.Infrastructure;
using Dynamo.Repository.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DecathlonDynamoErpApp.Controllers.CICalendars
{
    [Authorize]
    public class CICalendarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IHubContext<SignalServer> _hubContext;
        private readonly INotificationRepository _notificationRepository;

        public CICalendarController(UserManager<ApplicationUser> userManager, INotificationRepository notificationRepository, IHubContext<SignalServer> hubContext, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> Index()
        {
            //Get All user for a factory
            var allUsers = _context.Users.Where(m => m.UserName != HttpContext.User.Identity.Name).ToList();

            ViewData["AllUsers"] = allUsers;
            ViewData["CurrentUserId"] = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id; ;
            return View();
        }

        public async Task<JsonResult> Save(string data)
        {
            var calendarEventViewModel = JsonConvert.DeserializeObject<CalendarEventViewModel>(data);

            var calendarEvent = new CalendarEvent();
            calendarEvent.Title = calendarEventViewModel.Title;
            calendarEvent.FromDate = calendarEventViewModel.FromDate;
            calendarEvent.ToDate = calendarEventViewModel.ToDate;
            calendarEvent.StatusColour = calendarEventViewModel.StatusColour;
            calendarEvent.Description = calendarEventViewModel.Description;
            calendarEvent.AllDay = calendarEventViewModel.AllDay;
            calendarEvent.CreatedByUserId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
            calendarEvent.EventGuests = new List<EventGuest>();
            foreach (var guestUserId in calendarEventViewModel.GuestUserId)
            {
                //var user = (await _userManager.FindByIdAsync(guestUserId));
                if (calendarEventViewModel.Id != 0)
                {
                    if (!_context.EventGuests.Any(e => e.CalendarEventId == calendarEventViewModel.Id && e.UserId == guestUserId))
                    {
                        EventGuest eventGuest = new EventGuest();
                        eventGuest.UserId = guestUserId;
                        eventGuest.Status = 1;
                        calendarEvent.EventGuests.Add(eventGuest);

                        //Send Notification
                        Notification notification = new Notification();
                        notification.AdderId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
                        notification.LinkAction = "Index";
                        notification.LinkController = "CICalendar";
                        notification.PostingTime = DateTime.Now;
                        notification.NotificationTypeId = 2;
                        notification.Text = "You are invited to an event named - " + calendarEvent.Title;
                        _notificationRepository.CreateByUserId(notification, guestUserId);
                    }
                }
                else
                {
                    EventGuest eventGuest = new EventGuest();
                    eventGuest.UserId = guestUserId;
                    eventGuest.Status = 1;
                    calendarEvent.EventGuests.Add(eventGuest);

                    //Send Notification
                    Notification notification = new Notification();
                    notification.AdderId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
                    notification.LinkAction = "Index";
                    notification.LinkController = "CICalendar";
                    notification.PostingTime = DateTime.Now;
                    notification.NotificationTypeId = 2;
                    notification.Text = "You are invited to an event named - " + calendarEvent.Title;
                    _notificationRepository.CreateByUserId(notification, guestUserId);
                }



            }


            if (calendarEventViewModel.Id != 0)
            {
                calendarEvent.Id = calendarEventViewModel.Id;
                _context.CalendarEvents.Update(calendarEvent);
                _context.SaveChanges();
                return new JsonResult(true);
            }
            else
            {
                _context.CalendarEvents.Add(calendarEvent);
                _context.SaveChanges();
                return new JsonResult(true);
            }

        }

        public async Task<JsonResult> GetAll()
        {
            //Filter Events from Here
            var cuurentUserId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
            List<CalendarEvent> calendarEvents = _context.CalendarEvents.Include(m => m.EventGuests).ToList();
            List<CalendarEvent> filteredEvents = new List<CalendarEvent>();

            foreach (var calendarEvent in calendarEvents)
            {
                if (calendarEvent.CreatedByUserId == cuurentUserId)
                {
                    filteredEvents.Add(calendarEvent);
                }
                else
                {
                    if (calendarEvent.EventGuests != null)
                    {
                        foreach (var eventGuest in calendarEvent.EventGuests)
                        {
                            if (eventGuest.UserId == cuurentUserId && eventGuest.Status != 3)
                            {
                                filteredEvents.Add(calendarEvent);
                            }
                        }
                    }
                }
            }

            List<FullCalendarEventViewModel> events = new List<FullCalendarEventViewModel>();
            var dockingSchedulesEvents = filteredEvents.ConvertAll(x => new FullCalendarEventViewModel
            {
                id = x.Id,
                allDay = x.AllDay,
                //start = DateTime.Parse(x.FromDate).AddDays(1).ToString("MM-dd-yyyy"),
                start = DateTime.Parse(x.FromDate).ToShortDateString(),
                end = DateTime.Parse(x.ToDate).AddDays(1).ToShortDateString(),
                className = x.StatusColour,
                description = x.Description,
                editable = false,
                disabled = false,
                title = x.Title
            });
            events.AddRange(dockingSchedulesEvents);



            return new JsonResult(events);
        }

        [HttpPost]
        public JsonResult Find(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }

            var calendarEvent = _context.CalendarEvents.Include(m => m.EventGuests).FirstOrDefault(m => m.Id == id);
            if (calendarEvent == null)
            {
                return Json(false);
            }
            else
            {
                return Json(calendarEvent);
            }

        }

        [HttpPost]
        public async Task<JsonResult> DeleteEvent(int id)
        {
            var calendarEvent = await _context.CalendarEvents.Include(m => m.EventGuests).FirstOrDefaultAsync(m => m.Id == id);
            _context.CalendarEvents.Remove(calendarEvent);
            return Json(await _context.SaveChangesAsync());
        }

        [HttpPost]
        public async Task<JsonResult> AcceptEvent(int eventId, string userId)
        {
            var event1 = _context.CalendarEvents.FirstOrDefault(m => m.Id == eventId);
            var eventGuest = await _context.EventGuests.FirstOrDefaultAsync(m => m.UserId == userId && m.CalendarEventId == eventId);
            eventGuest.Status = 2;
            _context.EventGuests.Update(eventGuest);
            try
            {
                await _context.SaveChangesAsync();
                //Send Notification
                Notification notification = new Notification();
                notification.AdderId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
                notification.LinkAction = "Index";
                notification.LinkController = "CICalendar";
                notification.PostingTime = DateTime.Now;
                notification.NotificationTypeId = 2;
                notification.Text = HttpContext.User.Identity.Name + " accepted your event named - " + event1.Title;
                _notificationRepository.CreateByUserId(notification, event1.CreatedByUserId);
                return new JsonResult(true);
            }
            catch
            {
                return new JsonResult(false);
            }

        }

        [HttpPost]
        public async Task<JsonResult> RejectEvent(int eventId, string userId)
        {
            var event1 = _context.CalendarEvents.FirstOrDefault(m => m.Id == eventId);
            var eventGuest = await _context.EventGuests.FirstOrDefaultAsync(m => m.UserId == userId && m.CalendarEventId == eventId);
            eventGuest.Status = 3;
            _context.EventGuests.Update(eventGuest);
            try
            {
                await _context.SaveChangesAsync();
                //Send Notification
                Notification notification = new Notification();
                notification.AdderId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
                notification.LinkAction = "Index";
                notification.LinkController = "CICalendar";
                notification.PostingTime = DateTime.Now;
                notification.NotificationTypeId = 2;
                notification.Text = HttpContext.User.Identity.Name + " rejected your event named - " + event1.Title;
                _notificationRepository.CreateByUserId(notification, event1.CreatedByUserId);
                return new JsonResult(true);
            }
            catch
            {
                return new JsonResult(false);
            }
        }

    }
}