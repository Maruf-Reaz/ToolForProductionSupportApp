using System;
using System.Linq;
using System.Threading.Tasks;
using DecathlonDynamoErpApp.Models.ViewModel;
using Dynamo.Data;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Common.File;
using Dynamo.Model.Common.UserProfiles;
using Dynamo.Model.Common.UserProfiles.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecathlonDynamoErpApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AccountController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> GetConnectedPeople()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var users = _userManager.Users.Include(m => m.UserProfile).Where(m => m.FactoryId == loggedInUser.FactoryId && m.Id != "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508");

            return View(users);
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //Find User
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, "SuperAdmin"))
                        {
                            return RedirectToAction("SuperAdminIndex", "Home");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Provided Password is Incorrect!");
                    }
                }
                else
                {
                    ModelState.TryAddModelError("", "The UserName or Password Provided is Incorrect");
                }
            }

            //Not found user or password did not matched            
            return View(loginViewModel);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> FactoryLogin(string userId, int factoryId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var factory = await _context.Factories.FirstOrDefaultAsync(m => m.Id == factoryId);
            if (user == null || factory == null)
            {
                return NotFound();
            }
            else
            {
                user.FactoryId = factoryId;
                var result = await _userManager.UpdateAsync(user);

                //Redirect User
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name); //LoggedIn User

                //Create user
                var user = new ApplicationUser()
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FactoryId = loggedInUser.FactoryId
                };
                //Create user with password
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                //Redirect User
                if (result.Succeeded)
                {
                    var userProfile = new UserProfile
                    {
                        ApplicationUserId = user.Id,
                        PhotoName = "No File"
                    };
                    _context.Add(userProfile);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Users");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(registerViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(string userId)
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

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user.Id != loggedInUser.Id)
            {
                return NotFound();
            }

            var updatePasswordViewModel = new UpdatePasswordViewModel
            {
                UserId = user.Id
            };

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(string userId, UpdatePasswordViewModel updatePasswordViewModel)
        {

            if (userId == null)
            {
                return NotFound();
            }

            if (userId != updatePasswordViewModel.UserId)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Update password of user
                var result = await _userManager.ChangePasswordAsync(user, updatePasswordViewModel.PreviousPassword, updatePasswordViewModel.NewPassword);
                //Redirect User
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Users");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(updatePasswordViewModel);
        }

        // GET: ELearnings/UpdateProfile/5
        [Authorize]
        public async Task<IActionResult> UpdateProfile(string userId)
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

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user.Id != loggedInUser.Id)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(m => m.ApplicationUserId == user.Id);
            if (userProfile == null)
            {
                return NotFound();
            }

            var userProfileViewModel = new UserProfileViewModel
            {
                Id = userProfile.Id,
                ApplicationUserId = userProfile.ApplicationUserId,
                PhoneNumber = userProfile.PhoneNumber,
                City = userProfile.City,
                Nationality = userProfile.Nationality,
                OldPhotoName = userProfile.PhotoName
            };

            return View(userProfileViewModel);
        }

        // POST: ELearnings/UpdateProfile/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(int id, UserProfileViewModel userProfileViewModel)
        {
            if (id != userProfileViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                InputFile fileUpload = new InputFile(_hostingEnvironment);
                var userProfile = new UserProfile
                {
                    Id = userProfileViewModel.Id,
                    ApplicationUserId = userProfileViewModel.ApplicationUserId,
                    PhoneNumber = userProfileViewModel.PhoneNumber,
                    City = userProfileViewModel.City,
                    Nationality = userProfileViewModel.Nationality,
                    PhotoName = userProfileViewModel.OldPhotoName
                };

                if (userProfileViewModel.Photo != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = userProfileViewModel.Photo.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    userProfile.PhotoName = uniqueFileName;
                    if (userProfileViewModel.OldPhotoName.ToLower() == "no file")
                    {
                        fileUpload.Uploadfile("files/user_profiles", userProfileViewModel.Photo, uniqueFileName);
                    }
                    else
                    {
                        fileUpload.Updatefile("files/user_profiles", userProfileViewModel.Photo, userProfileViewModel.OldPhotoName, uniqueFileName);
                    }
                }

                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index", "Home");
            }

            return View(userProfileViewModel);
        }

    }
}