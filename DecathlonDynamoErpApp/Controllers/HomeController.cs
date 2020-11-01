using Microsoft.AspNetCore.Mvc;
using Dynamo.Data;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Dynamo.Model.Common.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dynamo.Model.Common.Infrastructure;
using System;

namespace DecathlonDynamoErpApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult SuperAdminIndex()
        {
            var factories = _context.Factories.ToList();
            return View(factories);
        }

        public IActionResult Slider()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestTable()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> Conversation()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name); //LoggedIn User
            var userId = (await _userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id; //LoggedIn User Id

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
                var unreadMessages = _context.Chats.Where(b => b.SentTo == userId && b.SentBy == user.Id)
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
                    userChatViewModel.PhotoName = user.UserProfile.PhotoName;
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
                    userChatViewModel.PhotoName = user.UserProfile.PhotoName;
                    chatUsers.Add(userChatViewModel);
                }
            }

            var finalChatUsers = chatUsers.OrderByDescending(t => t.LastChatTime);
            var lastChatUser = finalChatUsers.FirstOrDefault();

            if (lastChatUser != null)
            {
                var lastUserChats = await _context.Chats.Where(b => (b.SentTo == lastChatUser.UserId && b.SentBy == userId) || (b.SentBy == lastChatUser.UserId && b.SentTo == userId))
                .OrderBy(t => t.Time)
                .ToListAsync();
                ViewData["LastUserChats"] = lastUserChats;
            }
            else
            {
                ViewData["LastUserChats"] = null;
            }

            ViewData["ChatUsers"] = finalChatUsers;
            ViewData["LastChatUser"] = lastChatUser;
            ViewData["CurrentUser"] = userId;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/Home/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    break;
            }
            return View();
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> SkillMatrixRanges()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var skillMatrixRanges = await _context.SkillMatrixRanges.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();

            var ranges = skillMatrixRanges.OrderBy(m => m.Code);
            ViewData["SkillMatrixRanges"] = ranges;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<JsonResult> SaveRange(int? rangeId, string level)
        {
            if (rangeId == null)
            {
                return new JsonResult(false);
            }

            var skilRange = await _context.SkillMatrixRanges
                .Where(m => m.Id == rangeId)
                .FirstOrDefaultAsync();

            skilRange.Level = level;
            _context.SkillMatrixRanges.Update(skilRange);
            await _context.SaveChangesAsync();

            return new JsonResult(true);
        }


        //cookie theme
        public IActionResult SetTheme(string data)
        {
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(365)
            };

            Response.Cookies.Append("theme", data, cookies);

            return Ok();
        }
        
        //cookie sidebar theme
        public IActionResult SidebarTheme(string data)
        {
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(365)
            };
            
            Response.Cookies.Append("sidebartheme", data, cookies);

            return Ok();
        }
        
        //cookie sidebar style
        public IActionResult SidebarStyle(string data)
        {
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(365)
            };
            
            Response.Cookies.Append("sidebarstyle", data, cookies);

            return Ok();
        }

        //cookie sidebar
        public IActionResult Sidebar(string data)
        {
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(365)
            };
            
            Response.Cookies.Append("leftNavigation", data, cookies);

            return Ok();
        }
    }
}
