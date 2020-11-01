using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.ELearnings;
using Dynamo.Model.ELearnings.ViewModels;
using Dynamo.Model.Common.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;
using jQueryDatatableServerSideNetCore22.Models.AuxiliaryModels;
using jQueryDatatableServerSideNetCore22.Extensions;
using System.Collections.Generic;

namespace DecathlonDynamoErpApp.Controllers.ELearnings
{
    [Authorize]
    public class ELearningsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ELearningsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: ELearnings
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
            return View(await _context.ELearnings.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync());
        }

        // GET: ELearnings/Details/5
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

            var eLearning = await _context.ELearnings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eLearning == null)
            {
                return NotFound();
            }

            return View(eLearning);
        }

        // GET: ELearnings/Create
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ELearnings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Create(ELearningViewModel eLearningViewModel)
        {
            if (ModelState.IsValid)
            {
                string file1 = "No File";
                string file2 = "No File";
                if (eLearningViewModel.ELearningFile1 != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = eLearningViewModel.ELearningFile1.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    file1 = uniqueFileName;
                    InputFile fileUpload = new InputFile(_hostingEnvironment);
                    fileUpload.Uploadfile("files/e_learnings", eLearningViewModel.ELearningFile1, file1);

                }
                if (eLearningViewModel.ELearningFile2 != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = eLearningViewModel.ELearningFile2.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    file2 = uniqueFileName;
                    InputFile fileUpload = new InputFile(_hostingEnvironment);
                    fileUpload.Uploadfile("files/e_learnings", eLearningViewModel.ELearningFile2, file2);

                }

                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var eLearning = new ELearning
                {
                    FactoryId = loggedInUser.FactoryId,
                    Description = eLearningViewModel.Description,
                    DateOfUpload = eLearningViewModel.DateOfUpload,
                    ELearningFileName1 = file1,
                    ELearningFileName2 = file2
                };

                _context.Add(eLearning);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eLearningViewModel);
        }

        // GET: ELearnings/Edit/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eLearning = await _context.ELearnings.FindAsync(id);
            if (eLearning == null)
            {
                return NotFound();
            }

            var eLearningViewModel = new ELearningViewModel
            {
                Id = eLearning.Id,
                FactoryId = eLearning.FactoryId,
                Description = eLearning.Description,
                DateOfUpload = eLearning.DateOfUpload,
                OldELearningFileName1 = eLearning.ELearningFileName1,
                OldELearningFileName2 = eLearning.ELearningFileName2
            };

            return View(eLearningViewModel);
        }

        // POST: ELearnings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Edit(int id, ELearningViewModel eLearningViewModel)
        {
            if (id != eLearningViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                InputFile fileUpload = new InputFile(_hostingEnvironment);
                var eLearning = new ELearning
                {
                    Id = eLearningViewModel.Id,
                    FactoryId = eLearningViewModel.FactoryId,
                    Description = eLearningViewModel.Description,
                    DateOfUpload = eLearningViewModel.DateOfUpload,
                    ELearningFileName1 = eLearningViewModel.OldELearningFileName1,
                    ELearningFileName2 = eLearningViewModel.OldELearningFileName2
                };

                if (eLearningViewModel.ELearningFile1 != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = eLearningViewModel.ELearningFile1.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    eLearning.ELearningFileName1 = uniqueFileName;
                    if (eLearningViewModel.OldELearningFileName1.ToLower() == "no file")
                    {
                        fileUpload.Uploadfile("files/e_learnings", eLearningViewModel.ELearningFile1, uniqueFileName);
                    }
                    else
                    {
                        fileUpload.Updatefile("files/e_learnings", eLearningViewModel.ELearningFile1, eLearningViewModel.OldELearningFileName1, uniqueFileName);
                    }
                }
                if (eLearningViewModel.ELearningFile2 != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = eLearningViewModel.ELearningFile2.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    eLearning.ELearningFileName2 = uniqueFileName;
                    if (eLearningViewModel.OldELearningFileName2.ToLower() == "no file")
                    {
                        fileUpload.Uploadfile("files/e_learnings", eLearningViewModel.ELearningFile2, uniqueFileName);
                    }
                    else
                    {
                        fileUpload.Updatefile("files/e_learnings", eLearningViewModel.ELearningFile2, eLearningViewModel.OldELearningFileName2, uniqueFileName);
                    }
                }

                try
                {
                    _context.Update(eLearning);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ELearningExists(eLearning.Id))
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
            return View(eLearningViewModel);
        }

        // GET: ELearnings/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eLearning = await _context.ELearnings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eLearning == null)
            {
                return NotFound();
            }

            return View(eLearning);
        }

        // POST: ELearnings/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eLearning = await _context.ELearnings.FindAsync(id);

            InputFile fileUpload = new InputFile(_hostingEnvironment);
            if (eLearning.ELearningFileName1.ToLower() != "no file")
            {
                fileUpload.DeleteFile("files/e_learnings", eLearning.ELearningFileName1);
            }
            if (eLearning.ELearningFileName2.ToLower() != "no file")
            {
                fileUpload.DeleteFile("files/e_learnings", eLearning.ELearningFileName2);
            }

            _context.ELearnings.Remove(eLearning);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ELearningExists(int id)
        {
            return _context.ELearnings.Any(e => e.Id == id);
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
            var result = await _context.ELearnings.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();

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
            var totalResultsCount = await _context.ELearnings.Where(m => m.FactoryId == loggedInUser.FactoryId).CountAsync();
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
