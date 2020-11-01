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
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DecathlonDynamoErpApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IHubContext<SignalServer> _hubContext;
        private readonly INotificationRepository _notificationRepository;

        public ChatController(UserManager<ApplicationUser> userManager, INotificationRepository notificationRepository, IHubContext<SignalServer> hubContext, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> AddMessage(string sendToId, string messageBody)
        {

            var userId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id; //same thing
            var sentToUSer = _context.Users.Where(m => m.Id == sendToId)
                .FirstOrDefault();
            Chat chat = new Chat();
            chat.SentBy = userId;
            chat.SentByUserName = HttpContext.User.Identity.Name;
            chat.SentTo = sendToId;
            chat.SentToUserName = sentToUSer.UserName;
            chat.IsRead = false;
            chat.Time = DateTime.Now;
            chat.Message = messageBody;
            _context.Add(chat);
            await _context.SaveChangesAsync();

            return Json(true);
        }

        public async Task<JsonResult> GetChats(string otherUserId)
        {
            var userId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id; //same thing
            var tempUserChats = _context.Chats.Where(b => (b.SentTo == otherUserId && b.SentBy == userId) || (b.SentBy == otherUserId && b.SentTo == userId))
               .OrderBy(t => t.Time)
               .ToList();

            var lastUserChats = tempUserChats.Skip(Math.Max(0, tempUserChats.Count() - 50));

            foreach (var chat in tempUserChats)
            {
                if (userId == chat.SentTo && !chat.IsRead)
                {
                    chat.IsRead = true;
                    _context.Update(chat);
                    await _context.SaveChangesAsync();
                }
            }

            return Json(lastUserChats);
        }

        public async Task<JsonResult> GetAllChats(string otherUserId)
        {
            var userId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id; //same thing
            var lastUserChats = _context.Chats.Where(b => (b.SentTo == otherUserId && b.SentBy == userId) || (b.SentBy == otherUserId && b.SentTo == userId))
               .OrderBy(t => t.Time)
               .ToList();

            foreach (var chat in lastUserChats)
            {
                if (userId == chat.SentTo)
                {
                    chat.IsRead = true;
                    _context.Update(chat);
                    await _context.SaveChangesAsync();
                }
            }

            return Json(lastUserChats);
        }

        public async Task<JsonResult> GetSendToUserName(string sendToId)
        {
            var user = await _context.Users.Where(b => b.Id == sendToId).FirstOrDefaultAsync(); //same thing

            return Json(user);
        }

        public async Task<JsonResult> UpdateUsers()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name); //LoggedIn User
            var userId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id; //LoggedIn User ID

            var allUsers = await _userManager.Users
             .Where(b => b.Id != userId && b.FactoryId == loggedInUser.FactoryId && b.Id != "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507" && b.Id != "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508")
             .Include(m => m.UserProfile).ToListAsync();

            var nsUser = await _userManager.Users.Include(m => m.UserProfile).Where(m => m.Id == "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508").FirstOrDefaultAsync(); //NeuroStorm User
            var superAdminUser = await _userManager.Users.Include(m => m.UserProfile).Where(m => m.Id == "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507").FirstOrDefaultAsync(); //Monir Bhai User
            if (loggedInUser.Id == nsUser.Id)
            {
                allUsers.Add(superAdminUser);
            }
            else if (loggedInUser.Id == superAdminUser.Id)
            {
                allUsers.Add(nsUser);
            }
            else
            {
                allUsers.Add(nsUser);
                allUsers.Add(superAdminUser);
            }

            List<UserChatViewModel> chatUsers = new List<UserChatViewModel>();
            foreach (var user in allUsers)
            {
                UserChatViewModel userChatViewModel = new UserChatViewModel();
                var unreadMessages = _context.Chats.Where(b => b.SentTo == userId && b.SentBy == user.Id && !b.IsRead)
                    .ToList();
                if (unreadMessages.Count != 0)
                {
                    var lastChat = await _context.Chats.Where(b => (b.SentTo == user.Id && b.SentBy == userId) || (b.SentBy == user.Id && b.SentTo == userId))
                   .OrderByDescending(t => t.Time)
                   .FirstOrDefaultAsync();
                    if (lastChat.SentBy == userId)
                    {
                        userChatViewModel.UserId = lastChat.SentTo;
                    }
                    else if (lastChat.SentTo == userId)
                    {
                        userChatViewModel.UserId = lastChat.SentBy;
                    }
                    userChatViewModel.LastChatTime = lastChat.Time;
                    userChatViewModel.LastMessage = lastChat.Message;
                    userChatViewModel.UnReadChatNumber = unreadMessages.Count;
                    userChatViewModel.UserName = user.UserName;
                    chatUsers.Add(userChatViewModel);
                }
                else
                {
                    var lastChat = await _context.Chats.Where(b => (b.SentTo == user.Id && b.SentBy == userId) || (b.SentBy == user.Id && b.SentTo == userId))
                  .OrderByDescending(t => t.Time)
                  .FirstOrDefaultAsync();

                    if (lastChat != null)
                    {
                        userChatViewModel.LastChatTime = lastChat.Time;
                        userChatViewModel.LastMessage = lastChat.Message;

                    }
                    else
                    {
                        userChatViewModel.LastChatTime = null;
                        userChatViewModel.LastMessage = null;
                    }
                    userChatViewModel.UserId = user.Id;
                    userChatViewModel.UnReadChatNumber = 0;
                    userChatViewModel.UserName = user.UserName;
                    chatUsers.Add(userChatViewModel);
                }

            }
            var finalChatUsers = chatUsers.OrderByDescending(t => t.LastChatTime);
            return Json(finalChatUsers);
        }

        public async Task<JsonResult> GetChatNumber()
        {
            var userId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
            //var user = await _context.Users.Where(b => b.Id == sendToId).FirstOrDefaultAsync(); //same thing
            var chats = await _context.Chats
                .Where(b => b.SentTo == userId && !b.IsRead)
                .Select(b => b.SentBy)                             // Select only the county property
                .Distinct()
                .ToListAsync();

            return Json(chats.Count);
        }

    }
}