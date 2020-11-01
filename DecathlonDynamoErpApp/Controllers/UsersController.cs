using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamo.Data;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Common.File;
using Dynamo.Model.Common.ViewModels;
using jQueryDatatableServerSideNetCore22.Extensions;
using jQueryDatatableServerSideNetCore22.Models.AuxiliaryModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecathlonDynamoErpApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var users = _userManager.Users.Where(m => m.FactoryId == loggedInUser.FactoryId && m.Id != "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508");
            return View(users);
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var editUserViewModel = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles
            };

            return View(editUserViewModel);
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUserViewModel editUserViewModel)
        {
            if (id != editUserViewModel.Id)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.UserName = editUserViewModel.UserName;
                user.Email = editUserViewModel.Email;

                var result = await _userManager.UpdateAsync(user);

                //Redirect User
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Users");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(editUserViewModel);
            }
        }

        //Delete With Confirmation
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.Where(m => m.ApplicationUserId == user.Id).FirstOrDefaultAsync();
            var result = await _userManager.DeleteAsync(user);

            //Redirect User
            if (result.Succeeded)
            {
                if (userProfile.PhotoName.ToLower() != "no file")
                {
                    InputFile fileUpload = new InputFile(_hostingEnvironment);
                    fileUpload.DeleteFile("files/user_profiles", userProfile.PhotoName);
                }
                return RedirectToAction("Index", "Users");
            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction(nameof(Index));
        }

        //GET
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> EditRolesOfUser(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.userId = userId;
            ViewBag.userName = user.UserName;

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var roles = new List<ApplicationRole>();
            if (loggedInUser.FactoryId == 1)
            {
                roles = _roleManager.Roles.Where(m => m.NormalizedName == "KSIADMIN" || m.NormalizedName == "KSIUSEROFARCHIVE" || m.NormalizedName == "KSIUSEROFLINEBALANCING" || m.NormalizedName == "KSIUSEROFINCENTIVE" || m.NormalizedName == "KSIUSEROFSKILLMATRIX" || m.NormalizedName == "KSIUSEROFSOT" || m.NormalizedName == "KSIVIEWER").ToList();
            }
            if (loggedInUser.FactoryId == 2)
            {
                roles = _roleManager.Roles.Where(m => m.NormalizedName == "YSSADMIN" || m.NormalizedName == "YSSUSEROFARCHIVE" || m.NormalizedName == "YSSUSEROFLINEBALANCING" || m.NormalizedName == "YSSUSEROFINCENTIVE" || m.NormalizedName == "YSSUSEROFSKILLMATRIX" || m.NormalizedName == "YSSUSEROFSOT" || m.NormalizedName == "YSSVIEWER").ToList();
            }
            if (loggedInUser.FactoryId == 3)
            {
                roles = _roleManager.Roles.Where(m => m.NormalizedName == "MAFADMIN" || m.NormalizedName == "MAFUSEROFARCHIVE" || m.NormalizedName == "MAFUSEROFLINEBALANCING" || m.NormalizedName == "MAFUSEROFINCENTIVE" || m.NormalizedName == "MAFUSEROFSKILLMATRIX" || m.NormalizedName == "MAFUSEROFSOT" || m.NormalizedName == "MAFVIEWER").ToList();
            }
            if (loggedInUser.FactoryId == 4)
            {
                roles = _roleManager.Roles.Where(m => m.NormalizedName == "RFLADMIN" || m.NormalizedName == "RFLUSEROFARCHIVE" || m.NormalizedName == "RFLUSEROFLINEBALANCING" || m.NormalizedName == "RFLUSEROFINCENTIVE" || m.NormalizedName == "RFLUSEROFSKILLMATRIX" || m.NormalizedName == "RFLUSEROFSOT" || m.NormalizedName == "RFLVIEWER").ToList();
            }
            if (loggedInUser.FactoryId == 5)
            {
                roles = _roleManager.Roles.Where(m => m.NormalizedName == "APEXADMIN" || m.NormalizedName == "APEXUSEROFARCHIVE" || m.NormalizedName == "APEXUSEROFLINEBALANCING" || m.NormalizedName == "APEXUSEROFINCENTIVE" || m.NormalizedName == "APEXUSEROFSKILLMATRIX" || m.NormalizedName == "APEXUSEROFSOT" || m.NormalizedName == "APEXVIEWER").ToList();
            }
            if (loggedInUser.FactoryId == 6)
            {
                roles = _roleManager.Roles.Where(m => m.NormalizedName == "EDISONADMIN" || m.NormalizedName == "EDISONUSEROFARCHIVE" || m.NormalizedName == "EDISONUSEROFLINEBALANCING" || m.NormalizedName == "EDISONUSEROFINCENTIVE" || m.NormalizedName == "EDISONUSEROFSKILLMATRIX" || m.NormalizedName == "EDISONUSEROFSOT" || m.NormalizedName == "EDISONVIEWER").ToList();
            }

            var userRoleViewModels = new List<UserRoleViewModel>();
            foreach (var role in roles)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                userRoleViewModels.Add(userRoleViewModel);
            }

            return View(userRoleViewModels);
        }

        //POST
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> EditRolesOfUser(string userId, List<UserRoleViewModel> userRoleViewModels)
        {
            if (userId == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            for (int i = 0; i < userRoleViewModels.Count; i++)
            {
                var role = await _roleManager.FindByIdAsync(userRoleViewModels[i].RoleId);
                IdentityResult result = null;
                if (userRoleViewModels[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!userRoleViewModels[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < userRoleViewModels.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("Edit", new { Id = userId });
                    }
                }
            }

            return RedirectToAction("Edit", new { Id = userId });
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]DTParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                try
                {
                    orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                    orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                }
                catch (Exception)
                {

                    orderCriteria = "Id";
                    orderAscendingDirection = true;
                }
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            //here
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var result = await _userManager.Users.Where(m => m.FactoryId == loggedInUser.FactoryId && m.Id != "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508").ToListAsync();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.UserName != null && r.UserName.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()))
                    .ToList();
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();

            var filteredResultsCount = result.Count();

            //here
            var totalResultsCount = await _userManager.Users.Where(m => m.FactoryId == loggedInUser.FactoryId && m.Id != "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508").CountAsync();
            if (dtParameters.Length == -1)
            {
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = result
                   .Skip(dtParameters.Start)
                   .ToList()
                });
            }
            else
            {
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = result
                  .Skip(dtParameters.Start)
                  .Take(dtParameters.Length)
                  .ToList()
                });
            }
        }
    }
}