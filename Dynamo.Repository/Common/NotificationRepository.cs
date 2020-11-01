using Dynamo.Data;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Common.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Repository.Common
{

    public interface INotificationRepository
    {
        List<NotificationApplicationUser> GetUserNotifications(string userId);
        void CreateByGroupId(Notification notification, int gtopupId);
        void CreateByUserId(Notification notification, string userId);
        void CreateByUserList(Notification notification, List<ApplicationUser> users);
        void ReadNotification(int notificationId, string userId);
    }



    public class NotificationRepository : INotificationRepository
    {
        public ApplicationDbContext _context { get; }
        private IHubContext<SignalServer> _hubContext;
        public INotificationGroupRepository _notificationGroupRepository { get; }

        public NotificationRepository(ApplicationDbContext context, IHubContext<SignalServer> hubContext, INotificationGroupRepository notificationGroupRepository)
        {
            _context = context;
            _notificationGroupRepository = notificationGroupRepository;
            _hubContext = hubContext;
        }

        public void CreateByGroupId(Notification notification, int groupId)
        {

            _context.Notifications.Add(notification);
            _context.SaveChanges();

            //TODO: Assign notification to users
            var notificationGroups = _notificationGroupRepository.GetNotificationGroupFromPGroupId(groupId);
            foreach (var notificationGroup in notificationGroups)
            {
                var userNotification = new NotificationApplicationUser();
                userNotification.UserId = notificationGroup.UserId;
                userNotification.NotificationId = notification.Id;

                _context.UserNotifications.Add(userNotification);
                _context.SaveChanges();
            }
        }

        public void CreateByUserList(Notification notification, List<ApplicationUser> users)
        {

            _context.Notifications.Add(notification);
            _context.SaveChanges();

            //TODO: Assign notification to users
          
            foreach (var user in users)
            {
                var userNotification = new NotificationApplicationUser();
                userNotification.UserId = user.Id;
                userNotification.NotificationId = notification.Id;
                _context.UserNotifications.Add(userNotification);
                _context.SaveChanges();
            }
        }


        public void CreateByUserId(Notification notification, string userId)
        {

            _context.Notifications.Add(notification);
            _context.SaveChanges();

            //TODO: Assign notification to users
           
                var userNotification = new NotificationApplicationUser();
                userNotification.UserId = userId;
                userNotification.NotificationId = notification.Id;

                _context.UserNotifications.Add(userNotification);
                _context.SaveChanges();
            
        }

        public List<NotificationApplicationUser> GetUserNotifications(string userId)
        {
            return _context.UserNotifications.Where(u => u.UserId.Equals(userId) && !u.IsRead)
                                            .Include(n => n.Notification)
                                            .ToList();
        }

        public void ReadNotification(int notificationId, string userId)
        {
            var notification = _context.UserNotifications
                                        .FirstOrDefault(n => n.UserId.Equals(userId)
                                        && n.NotificationId == notificationId);
            notification.IsRead = true;
            _context.UserNotifications.Update(notification);
            _context.SaveChanges();
        }

    }



}
