using Dynamo.Data;
using Dynamo.Model.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Repository.Common
{

    public interface INotificationGroupRepository
    {
       
        List<NotificationGroup> GetNotificationGroupFromPGroupId(int groupId);
    }


    public class NotificationGroupRepository : INotificationGroupRepository
    {
        private ApplicationDbContext _context;
        public NotificationGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<NotificationGroup> GetNotificationGroupFromPGroupId(int groupId)
        {
            return _context.NotificationGroups.Where(w => w.GroupId == groupId).ToList();
        }

    }
}
