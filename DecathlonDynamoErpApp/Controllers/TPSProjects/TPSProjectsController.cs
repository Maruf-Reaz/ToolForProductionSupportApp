using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.TPSProjects;
using Microsoft.AspNetCore.Authorization;
using Dynamo.Model.TPSProjects.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Dynamo.Model.Common.File;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;
using jQueryDatatableServerSideNetCore22.Models.AuxiliaryModels;
using jQueryDatatableServerSideNetCore22.Extensions;

namespace DecathlonDynamoErpApp.Controllers.TPSProjects
{
    [Authorize]
    public class TPSProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TPSProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: TPSProjects
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
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return View(await _context.TPSProjects.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync());
        }

        // GET: TPSProjects/Details/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tPSProject = await _context.TPSProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tPSProject == null)
            {
                return NotFound();
            }

            return View(tPSProject);
        }

        // GET: TPSProjects/Create
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TPSProjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Create(TPSProjectViewModel tPSProjectViewModel)
        {
            if (ModelState.IsValid)
            {
                string file1 = "No File";
                string file2 = "No File";
                if (tPSProjectViewModel.ProjectFile1 != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = tPSProjectViewModel.ProjectFile1.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    file1 = uniqueFileName;
                    InputFile fileUpload = new InputFile(_hostingEnvironment);
                    fileUpload.Uploadfile("files/tps_projects", tPSProjectViewModel.ProjectFile1, file1);

                }
                if (tPSProjectViewModel.ProjectFile2 != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = tPSProjectViewModel.ProjectFile2.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    file2 = uniqueFileName;
                    InputFile fileUpload = new InputFile(_hostingEnvironment);
                    fileUpload.Uploadfile("files/tps_projects", tPSProjectViewModel.ProjectFile2, file2);

                }

                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var tPSProject = new TPSProject
                {
                    FactoryId = loggedInUser.FactoryId,
                    Description = tPSProjectViewModel.Description,
                    DateOfUpload = tPSProjectViewModel.DateOfUpload,
                    ProjectFileName1 = file1,
                    ProjectFileName2 = file2
                };

                _context.Add(tPSProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tPSProjectViewModel);
        }

        // GET: TPSProjects/Edit/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tPSProject = await _context.TPSProjects.FirstOrDefaultAsync(m => m.Id == id);
            if (tPSProject == null)
            {
                return NotFound();
            }

            var tPSProjectViewModel = new TPSProjectViewModel
            {
                Id = tPSProject.Id,
                FactoryId = tPSProject.FactoryId,
                Description = tPSProject.Description,
                DateOfUpload = tPSProject.DateOfUpload,
                OldProjectFileName1 = tPSProject.ProjectFileName1,
                OldProjectFileName2 = tPSProject.ProjectFileName2
            };

            return View(tPSProjectViewModel);
        }

        // POST: TPSProjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Edit(int id, TPSProjectViewModel tPSProjectViewModel)
        {
            if (id != tPSProjectViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                InputFile fileUpload = new InputFile(_hostingEnvironment);
                var tPSProject = new TPSProject
                {
                    Id = tPSProjectViewModel.Id,
                    FactoryId = tPSProjectViewModel.FactoryId,
                    Description = tPSProjectViewModel.Description,
                    DateOfUpload = tPSProjectViewModel.DateOfUpload,
                    ProjectFileName1 = tPSProjectViewModel.OldProjectFileName1,
                    ProjectFileName2 = tPSProjectViewModel.OldProjectFileName2
                };

                if (tPSProjectViewModel.ProjectFile1 != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = tPSProjectViewModel.ProjectFile1.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    tPSProject.ProjectFileName1 = uniqueFileName;
                    if (tPSProjectViewModel.OldProjectFileName1.ToLower() == "no file")
                    {
                        fileUpload.Uploadfile("files/tps_projects", tPSProjectViewModel.ProjectFile1, uniqueFileName);
                    }
                    else
                    {
                        fileUpload.Updatefile("files/tps_projects", tPSProjectViewModel.ProjectFile1, tPSProjectViewModel.OldProjectFileName1, uniqueFileName);
                    }
                }
                if (tPSProjectViewModel.ProjectFile2 != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = tPSProjectViewModel.ProjectFile2.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    tPSProject.ProjectFileName2 = uniqueFileName;
                    if (tPSProjectViewModel.OldProjectFileName2.ToLower() == "no file")
                    {
                        fileUpload.Uploadfile("files/tps_projects", tPSProjectViewModel.ProjectFile1, uniqueFileName);
                    }
                    else
                    {
                        fileUpload.Updatefile("files/tps_projects", tPSProjectViewModel.ProjectFile2, tPSProjectViewModel.OldProjectFileName2, uniqueFileName);
                    }
                }

                try
                {
                    _context.Update(tPSProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TPSProjectExists(tPSProject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tPSProjectViewModel);
        }

        // GET: TPSProjects/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tPSProject = await _context.TPSProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tPSProject == null)
            {
                return NotFound();
            }

            return View(tPSProject);
        }

        // POST: TPSProjects/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tPSProject = await _context.TPSProjects.FindAsync(id);

            InputFile fileUpload = new InputFile(_hostingEnvironment);
            if (tPSProject.ProjectFileName1.ToLower() != "no file")
            {
                fileUpload.DeleteFile("files/tps_projects", tPSProject.ProjectFileName1);
            }
            if (tPSProject.ProjectFileName2.ToLower() != "no file")
            {
                fileUpload.DeleteFile("files/tps_projects", tPSProject.ProjectFileName2);
            }

            _context.TPSProjects.Remove(tPSProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TPSProjectExists(int id)
        {
            return _context.TPSProjects.Any(e => e.Id == id);
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
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
            var result = await _context.TPSProjects.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => 
                r.Description != null && r.Description.ToUpper().Contains(searchBy.ToUpper()) ||
                r.DateOfUpload != null && r.DateOfUpload.ToString("dd-MM-yyyy").Contains(searchBy.ToString()))
                    .ToList();
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();

            var filteredResultsCount = result.Count();

            //here
            var totalResultsCount = await _context.TPSProjects.Where(m => m.FactoryId == loggedInUser.FactoryId).CountAsync();
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
