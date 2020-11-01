using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamo.Data;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Common.Infrastructure;
using Dynamo.Repository.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecathlonDynamoErpApp.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private INotificationRepository _notificationRepository;
        public NotificationController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, INotificationRepository notificationRepository)
        {
            _context = context;
            _notificationRepository = notificationRepository;
            _userManager = userManager;
        }

        public async Task<JsonResult> GetNotification()
        {

            var userId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
            var userNotifications = _context.UserNotifications.Where(u => u.UserId.Equals(userId) && !u.IsRead)
                                            .Include(n => n.Notification)
                                            .OrderByDescending(u => u.Notification.PostingTime)
                                            .ToList();
            List<NotificationViewModel> sendAbleNotifications = new List<NotificationViewModel>();



            foreach (var userNotification in userNotifications)
            {
                if (sendAbleNotifications.Count>4)
                {
                    break;
                }
                var NotificationAdder = await _userManager.FindByIdAsync(userNotification.Notification.AdderId);
                NotificationViewModel notificationViewModel = new NotificationViewModel();
                notificationViewModel.NotificationId = userNotification.NotificationId;
                notificationViewModel.NotificationText = userNotification.Notification.Text;
                notificationViewModel.NotificationTypeId = userNotification.Notification.NotificationTypeId;
                notificationViewModel.AdderId = userNotification.UserId;
                if (userNotification.Notification.PostingTime.Day == DateTime.Now.Day && userNotification.Notification.PostingTime.Month == DateTime.Now.Month
                    && userNotification.Notification.PostingTime.Year == DateTime.Now.Year)
                {
                    notificationViewModel.PostingTime = userNotification.Notification.PostingTime.ToString("h:mm tt");
                }
                else
                {
                    notificationViewModel.PostingTime = userNotification.Notification.PostingTime.ToString("MMMM dd h:mm tt");
                }

                notificationViewModel.AdderName = NotificationAdder.UserName;
                sendAbleNotifications.Add(notificationViewModel);

            }
            

            return Json(sendAbleNotifications);

        }

        public async Task<IActionResult> ReadNotification(int notificationId)
        {

            var notification = await _context.Notifications.FindAsync(notificationId);
            _notificationRepository.ReadNotification(notificationId, _userManager.GetUserId(HttpContext.User));

            return RedirectToAction(notification.LinkAction, notification.LinkController);
           

        }

        public async Task<IActionResult> ReadSingleNotification(int notificationId)
        {

            var notification = await _context.Notifications.FindAsync(notificationId);
            return RedirectToAction(notification.LinkAction, notification.LinkController);


        }


        public async Task<IActionResult> Show()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var userNotifications = _context.UserNotifications.Where(u => u.UserId.Equals(userId))
                                            .Include(n => n.Notification)
                                            .OrderByDescending(u => u.Notification.PostingTime)
                                            .ToList();
            List<NotificationViewModel> sendAbleNotifications = new List<NotificationViewModel>();



            foreach (var userNotification in userNotifications)
            {
               
                var NotificationAdder = await _userManager.FindByIdAsync(userNotification.Notification.AdderId);
                NotificationViewModel notificationViewModel = new NotificationViewModel();
                notificationViewModel.NotificationId = userNotification.NotificationId;
                notificationViewModel.NotificationText = userNotification.Notification.Text;
                notificationViewModel.NotificationLink = userNotification.Notification.LinkController+"/"+ userNotification.Notification.LinkAction;
                notificationViewModel.AdderId = userNotification.UserId;
                notificationViewModel.IsRead = userNotification.IsRead;
                if (userNotification.Notification.PostingTime.Day == DateTime.Now.Day && userNotification.Notification.PostingTime.Month == DateTime.Now.Month
                    && userNotification.Notification.PostingTime.Year == DateTime.Now.Year)
                {
                    notificationViewModel.PostingTime = userNotification.Notification.PostingTime.ToString("h:mm tt");
                }
                else
                {
                    notificationViewModel.PostingTime = userNotification.Notification.PostingTime.ToString("MMMM dd h:mm tt");
                }

                notificationViewModel.AdderName = NotificationAdder.UserName;
                sendAbleNotifications.Add(notificationViewModel);

            }


            return View(sendAbleNotifications);
        }


    }

}