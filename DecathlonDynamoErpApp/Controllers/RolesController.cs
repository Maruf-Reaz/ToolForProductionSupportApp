using Dynamo.Model.Common.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DecathlonDynamoErpApp.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // GET
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST
        //[HttpPost]
        //public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var applicationRole = new ApplicationRole
        //        {
        //            Name = roleViewModel.RoleName,
        //            Description = roleViewModel.Description,
        //            CreatedOn = roleViewModel.CreatedOn
        //        };
        //        IdentityResult result = await _roleManager.CreateAsync(applicationRole);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Roles");
        //        }
        //        else
        //        {
        //            foreach (IdentityError error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //        }
        //    }

        //    return View(roleViewModel);
        //}

        //GET
        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    var roleViewModel = new RoleViewModel
        //    {
        //        Id = role.Id,
        //        RoleName = role.Name,
        //        Description = role.Description,
        //        CreatedOn = role.CreatedOn
        //    };
        //    foreach (var user in _userManager.Users)
        //    {
        //        if (await _userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            roleViewModel.UserNames.Add(user.UserName);
        //        }
        //    }

        //    return View(roleViewModel);
        //}

        // POST
        //[HttpPost]
        //public async Task<IActionResult> Edit(string id, RoleViewModel roleViewModel)
        //{
        //    if (id != roleViewModel.Id)
        //    {
        //        return NotFound();
        //    }
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            role.Name = roleViewModel.RoleName;
        //            role.Description = roleViewModel.Description;
        //            role.CreatedOn = roleViewModel.CreatedOn;

        //            IdentityResult result = await _roleManager.UpdateAsync(role);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Index", "Roles");
        //            }
        //            else
        //            {
        //                foreach (IdentityError error in result.Errors)
        //                {
        //                    ModelState.AddModelError("", error.Description);
        //                }
        //            }
        //        }
        //        return View(roleViewModel);
        //    }
        //}

        //GET
        //[HttpGet]
        //public async Task<IActionResult> EditUsersInRole(string roleId)
        //{
        //    if (roleId == null)
        //    {
        //        return NotFound();
        //    }
        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.roleId = roleId;
        //    ViewBag.roleName = role.Name;
        //    var userRoleViewModels = new List<UserRoleViewModel>();
        //    foreach (var user in _userManager.Users)
        //    {
        //        var userRoleViewModel = new UserRoleViewModel
        //        {
        //            UserId = user.Id,
        //            UserName = user.UserName
        //        };
        //        if (await _userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            userRoleViewModel.IsSelected = true;
        //        }
        //        else
        //        {
        //            userRoleViewModel.IsSelected = false;
        //        }
        //        userRoleViewModels.Add(userRoleViewModel);
        //    }

        //    return View(userRoleViewModels);
        //}

        //POST
        //[HttpPost]
        //public async Task<IActionResult> EditUsersInRole(string roleId, List<UserRoleViewModel> userRoleViewModels)
        //{
        //    if (roleId == null)
        //    {
        //        return NotFound();
        //    }
        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    for (int i = 0; i < userRoleViewModels.Count; i++)
        //    {
        //        var user = await _userManager.FindByIdAsync(userRoleViewModels[i].UserId);
        //        IdentityResult result = null;
        //        if (userRoleViewModels[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
        //        {
        //            result = await _userManager.AddToRoleAsync(user, role.Name);
        //        }
        //        else if (!userRoleViewModels[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
        //        {
        //            result = await _userManager.RemoveFromRoleAsync(user, role.Name);
        //        }
        //        else
        //        {
        //            continue;
        //        }

        //        if (result.Succeeded)
        //        {
        //            if (i < userRoleViewModels.Count - 1)
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                return RedirectToAction("Edit", new { Id = roleId });
        //            }
        //        }
        //    }

        //    return RedirectToAction("Edit", new { Id = roleId });
        //}
    }
}