using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.Sot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;

namespace DecathlonDynamoErpApp.Controllers.Sot
{
    [Authorize]
    public class StandardOperationTimesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StandardOperationTimesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StandardOperationTimes
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> Index(int? processId)
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            //FactoryId Here
            var processes = await _context.Processes.Where(m => m.FactoryId == loggedInUser.FactoryId).OrderBy(m => m.Id).ToListAsync();
            ViewData["ProcessId"] = processId;
            ViewData["Processes"] = processes;

            if (processId == null)
            {
                var standardOperationTimes = new List<StandardOperationTime>();
                return View(standardOperationTimes);
            }
            else
            {
                var process = await _context.Processes.FirstOrDefaultAsync(m => m.Id == processId);
                if (process == null)
                {
                    return NotFound();
                }
                //FactoryId Here
                var standardOperationTimes = await _context.StandardOperationTimes.Where(m => m.FactoryId == loggedInUser.FactoryId && m.SotModel.ProcessId == processId)
                .Include(s => s.SotModel).ThenInclude(m => m.Process)
                .Include(s => s.SotModel).ThenInclude(m => m.SignSport)
                .ToListAsync();

                foreach (var standardOperationTime in standardOperationTimes)
                {
                    var sectionStandardOperationTimes = _context.SectionStandardOperationTimes
                        .Where(m => m.StandardOperationTimeId == standardOperationTime.Id)
                        .Include(m => m.Section).ToList();

                    standardOperationTime.SectionStandardOperationTimes = sectionStandardOperationTimes;
                }
                //FactoryId Here
                var sections = await _context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId && m.ProcessId == processId).OrderBy(m => m.Id).ToListAsync();
                ViewData["Sections"] = sections;
                ViewData["CalculationStatuses"] = await _context.CalculationStatuses.ToListAsync();
                ViewData["ValidationStatuses"] = await _context.ValidationStatuses.ToListAsync();

                return View(standardOperationTimes);
            }
        }

        // GET: StandardOperationTimes/Details/5
        //[HttpGet]
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var standardOperationTime = await _context.StandardOperationTimes
        //        .Include(s => s.SotModel).ThenInclude(m => m.Process)
        //        .Include(s => s.SotModel).ThenInclude(m => m.SignSport)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (standardOperationTime == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(standardOperationTime);
        //}

        // GET: StandardOperationTimes/Create
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfSOT,YSSUserOfSOT,MAFUserOfSOT,RFLUserOfSOT,APEXUserOfSOT,EDISONUserOfSOT")]
        public async Task<IActionResult> Create()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var sotModels = await _context.SotModels.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();
            ViewData["SotModels"] = sotModels;

            return View();
        }

        // POST: StandardOperationTimes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfSOT,YSSUserOfSOT,MAFUserOfSOT,RFLUserOfSOT,APEXUserOfSOT,EDISONUserOfSOT")]
        public async Task<IActionResult> Create([Bind("Id,SotModelId")] StandardOperationTime standardOperationTime)
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (ModelState.IsValid)
            {
                standardOperationTime.FactoryId = loggedInUser.FactoryId;
                _context.Add(standardOperationTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var sotModels = await _context.SotModels.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();
            ViewData["SotModels"] = sotModels;

            return View(standardOperationTime);
        }

        // GET: StandardOperationTimes/Edit/5
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfSOT,YSSUserOfSOT,MAFUserOfSOT,RFLUserOfSOT,APEXUserOfSOT,EDISONUserOfSOT")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardOperationTime = await _context.StandardOperationTimes
                .Include(s => s.SotModel).ThenInclude(m => m.Process)
                .Include(s => s.SotModel).ThenInclude(m => m.SignSport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standardOperationTime == null)
            {
                return NotFound();
            }

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["SotModelId"] = new SelectList(_context.SotModels.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", standardOperationTime.SotModelId);

            return View(standardOperationTime);
        }

        // POST: StandardOperationTimes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfSOT,YSSUserOfSOT,MAFUserOfSOT,RFLUserOfSOT,APEXUserOfSOT,EDISONUserOfSOT")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FactoryId,SotModelId,CalculationStatusId,ValidationStatusId")] StandardOperationTime standardOperationTime)
        {
            if (id != standardOperationTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standardOperationTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandardOperationTimeExists(standardOperationTime.Id))
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

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["SotModelId"] = new SelectList(_context.SotModels.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", standardOperationTime.SotModelId);

            return View(standardOperationTime);
        }

        // GET: StandardOperationTimes/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardOperationTime = await _context.StandardOperationTimes
                .Include(s => s.SotModel).ThenInclude(m => m.Process)
                .Include(s => s.SotModel).ThenInclude(m => m.SignSport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standardOperationTime == null)
            {
                return NotFound();
            }

            return View(standardOperationTime);
        }

        // POST: StandardOperationTimes/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var standardOperationTime = await _context.StandardOperationTimes.FirstOrDefaultAsync(m => m.Id == id);
            _context.StandardOperationTimes.Remove(standardOperationTime);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool StandardOperationTimeExists(int id)
        {
            return _context.StandardOperationTimes.Any(e => e.Id == id);
        }

        // GET: StandardOperationTimes/AddSection/5/6
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> AddSection(int? sotId, int? sectionId)
        {
            if (sotId == null || sectionId == null)
            {
                return NotFound();
            }

            var standardOperationTime = await _context.StandardOperationTimes.Include(s => s.SotModel).FirstOrDefaultAsync(m => m.Id == sotId);

            if (standardOperationTime == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FirstOrDefaultAsync(m => m.Id == sectionId);

            if (sectionId != 0 && section == null)
            {
                return NotFound();
            }

            var standardOperationTimeItems = new List<StandardOperationTimeItem>();
            if (section != null)
            {
                standardOperationTimeItems = await _context.StandardOperationTimeItems
               .Include(m => m.DataSource)
               .Include(m => m.Operation).ThenInclude(d => d.Machine)
               .Where(m => m.StandardOperationTimeId == standardOperationTime.Id)
               .Where(m => m.SectionId == section.Id)
               .ToListAsync();
            }

            standardOperationTime.StandardOperationTimeItems = standardOperationTimeItems;

            var sections = await _context.Sections.ToListAsync();
            var sectionStandardOperationTimes = _context.SectionStandardOperationTimes
                    .Where(m => m.StandardOperationTimeId == standardOperationTime.Id).ToList();

            foreach (var sectionStandardOperationTime in sectionStandardOperationTimes)
            {
                var sotSection = await _context.Sections.FirstOrDefaultAsync(m => m.Id == sectionStandardOperationTime.SectionId);
                sections.Remove(sotSection);
            }

            if (section != null)
            {
                sections.Add(section);
            }

            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Name", standardOperationTime.SotModel.ProcessId);
            ViewData["SignSportId"] = new SelectList(_context.SignSports, "Id", "Name", standardOperationTime.SotModel.SignSportId);
            ViewData["SotModelId"] = new SelectList(_context.SotModels, "Id", "Name", standardOperationTime.SotModelId);
            ViewData["Sections"] = sections;
            ViewData["Section"] = section;
            ViewData["DataSources"] = await _context.DataSources.ToListAsync();

            return View(standardOperationTime);
        }

        // GET: StandardOperationTimes/AddSotItem/5/6/7/8
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> AddSotItem(int? sotId, int? dataSourceId, int? sectionId, int? sotItemId)
        {
            if (sotId == null || dataSourceId == null || sectionId == null || sotItemId == null)
            {
                return NotFound();
            }

            var standardOperationTime = await _context.StandardOperationTimes.Include(m => m.SotModel).FirstOrDefaultAsync(m => m.Id == sotId);
            var dataSource = await _context.DataSources.FirstOrDefaultAsync(m => m.Id == dataSourceId);
            var section = await _context.Sections.FirstOrDefaultAsync(m => m.Id == sectionId);
            var sotItem = await _context.StandardOperationTimeItems
                .Include(m => m.Operation).ThenInclude(d => d.Machine)
                .Include(m => m.StandardOperationTimeSubItems)
                .FirstOrDefaultAsync(m => m.Id == sotItemId);

            if (standardOperationTime == null || dataSource == null || section == null)
            {
                return NotFound();
            }

            if (sotItemId != 0 && sotItem == null)
            {
                return NotFound();
            }

            ViewData["SotId"] = sotId;
            ViewData["DataSourceId"] = dataSourceId;
            ViewData["SectionId"] = sectionId;
            ViewData["SotItem"] = sotItem;
            ViewData["Sot"] = standardOperationTime;
            ViewData["Section"] = section;

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var manualJobs = await _context.ManualJobs.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();
            ViewData["ManualJobs"] = manualJobs;

            if (sotItem == null)
            {
                ViewData["OperationId"] = new SelectList(_context.Operations.Where(m => m.FactoryId == loggedInUser.FactoryId && m.SectionId == sectionId), "Id", "Name");
            }
            else
            {
                ViewData["OperationId"] = new SelectList(_context.Operations.Where(m => m.FactoryId == loggedInUser.FactoryId && m.SectionId == sectionId), "Id", "Name", sotItem.OperationId);

                var manualJobStandardOperationTimeItems = _context.ManualJobStandardOperationTimeItems
                    .Where(m => m.StandardOperationTimeItemId == sotItem.Id)
                    .Include(m => m.ManualJob).ToList();

                sotItem.ManualJobStandardOperationTimeItems = manualJobStandardOperationTimeItems;
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfSOT,YSSUserOfSOT,MAFUserOfSOT,RFLUserOfSOT,APEXUserOfSOT,EDISONUserOfSOT")]
        public async Task<JsonResult> AddSotItem(StandardOperationTimeItem standardOperationTimeItem)
        {
            var sotItemId = 0;
            if (standardOperationTimeItem.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(standardOperationTimeItem);
                    await _context.SaveChangesAsync();
                    sotItemId = standardOperationTimeItem.Id;

                    var sectionStandardOperationTime = await _context.SectionStandardOperationTimes
                        .FirstOrDefaultAsync(m => m.StandardOperationTimeId == standardOperationTimeItem.StandardOperationTimeId && m.SectionId == standardOperationTimeItem.SectionId);
                    if (sectionStandardOperationTime == null)
                    {
                        var newSectionStandardOperationTime = new SectionStandardOperationTime();
                        newSectionStandardOperationTime.StandardOperationTimeId = standardOperationTimeItem.StandardOperationTimeId;
                        newSectionStandardOperationTime.SectionId = standardOperationTimeItem.SectionId;
                        newSectionStandardOperationTime.SotValue = 0;

                        _context.Add(newSectionStandardOperationTime);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Update(standardOperationTimeItem);
                    await _context.SaveChangesAsync();
                    sotItemId = standardOperationTimeItem.Id;

                    var sectionStandardOperationTime = await _context.SectionStandardOperationTimes
                        .FirstOrDefaultAsync(m => m.StandardOperationTimeId == standardOperationTimeItem.StandardOperationTimeId && m.SectionId == standardOperationTimeItem.SectionId);
                    if (sectionStandardOperationTime == null)
                    {
                        var newSectionStandardOperationTime = new SectionStandardOperationTime();
                        newSectionStandardOperationTime.StandardOperationTimeId = standardOperationTimeItem.StandardOperationTimeId;
                        newSectionStandardOperationTime.SectionId = standardOperationTimeItem.SectionId;
                        newSectionStandardOperationTime.SotValue = 0;

                        _context.Add(newSectionStandardOperationTime);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return Json(sotItemId);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSotItem(int sotItemId)
        {
            var result = false;

            var standardOperationTimeItem = await _context.StandardOperationTimeItems
                    .Where(m => m.Id == sotItemId)
                    .FirstOrDefaultAsync();

            if (standardOperationTimeItem != null)
            {
                _context.Remove(standardOperationTimeItem);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddSerialNumberOfSotItem(int sotItemId, int serialNumber)
        {
            var result = false;

            var standardOperationTimeItem = await _context.StandardOperationTimeItems
                    .Where(m => m.Id == sotItemId)
                    .FirstOrDefaultAsync();

            if (standardOperationTimeItem != null)
            {
                standardOperationTimeItem.SerialNumber = serialNumber;
                _context.Update(standardOperationTimeItem);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetManualJobs(int sotItemId)
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var manualJobs = await _context.ManualJobs.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();

            var manualJobStandardOperationTimeItems = _context.ManualJobStandardOperationTimeItems
                    .Where(m => m.StandardOperationTimeItemId == sotItemId)
                    .Include(m => m.ManualJob).ToList();

            foreach (var manualJobStandardOperationTimeItem in manualJobStandardOperationTimeItems)
            {
                var manualJob = await _context.ManualJobs.Where(m => m.Id == manualJobStandardOperationTimeItem.ManualJobId).FirstOrDefaultAsync();
                manualJobs.Remove(manualJob);
            }

            return Json(manualJobs);
        }

        [HttpPost]
        public async Task<JsonResult> AddManualJob(ManualJobStandardOperationTimeItem manualJobStandardOperationTimeItem)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var newManualJobStandardOperationTimeItem = await _context.ManualJobStandardOperationTimeItems
                    .Where(m => m.StandardOperationTimeItemId == manualJobStandardOperationTimeItem.StandardOperationTimeItemId)
                    .Where(m => m.ManualJobId == manualJobStandardOperationTimeItem.ManualJobId)
                    .FirstOrDefaultAsync();

                if (newManualJobStandardOperationTimeItem == null)
                {
                    _context.Add(manualJobStandardOperationTimeItem);
                    int count = await _context.SaveChangesAsync();
                    if (count == 1)
                    {
                        result = true;
                    }
                }
                else
                {
                    newManualJobStandardOperationTimeItem.Time = manualJobStandardOperationTimeItem.Time;
                    newManualJobStandardOperationTimeItem.Quantity = manualJobStandardOperationTimeItem.Quantity;

                    _context.Update(newManualJobStandardOperationTimeItem);
                    int count = await _context.SaveChangesAsync();
                    if (count == 1)
                    {
                        result = true;
                    }
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteManualJob(int sotItemId, int manualJobId)
        {
            var result = false;
            var manualJobStandardOperationTimeItem = await _context.ManualJobStandardOperationTimeItems
                    .Where(m => m.StandardOperationTimeItemId == sotItemId)
                    .Where(m => m.ManualJobId == manualJobId)
                    .FirstOrDefaultAsync();

            if (manualJobStandardOperationTimeItem != null)
            {
                _context.Remove(manualJobStandardOperationTimeItem);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddStandardOperationTimeSubItem(StandardOperationTimeSubItem standardOperationTimeSubItem)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                if (standardOperationTimeSubItem.Id == 0)
                {
                    _context.Add(standardOperationTimeSubItem);
                    int count = await _context.SaveChangesAsync();
                    if (count == 1)
                    {
                        result = true;
                    }
                }
                else
                {
                    _context.Update(standardOperationTimeSubItem);
                    int count = await _context.SaveChangesAsync();
                    if (count == 1)
                    {
                        result = true;
                    }
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteStandardOperationTimeSubItem(int sotSubItemId)
        {
            var result = false;

            var standardOperationTimeSubItem = await _context.StandardOperationTimeSubItems
                    .Where(m => m.Id == sotSubItemId)
                    .FirstOrDefaultAsync();

            if (standardOperationTimeSubItem != null)
            {
                _context.Remove(standardOperationTimeSubItem);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> NeglectStandardOperationTimeSubItem(int sotSubItemId)
        {
            var result = false;

            var standardOperationTimeSubItem = await _context.StandardOperationTimeSubItems
                    .Where(m => m.Id == sotSubItemId)
                    .FirstOrDefaultAsync();

            if (standardOperationTimeSubItem != null)
            {
                standardOperationTimeSubItem.IsNeglected = true;
                _context.Update(standardOperationTimeSubItem);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCycleTimeOfSotItem(int sotItemId, double totalCycleTime, double rating, double allowance, double cyclePerPair)
        {
            var result = false;

            var standardOperationTimeItem = await _context.StandardOperationTimeItems
                    .Where(m => m.Id == sotItemId)
                    .FirstOrDefaultAsync();

            if (standardOperationTimeItem != null)
            {
                double sotValue = (((totalCycleTime * (rating / 100)) * (1 + (allowance / 100))) * cyclePerPair) / 60;

                standardOperationTimeItem.CycleTime = totalCycleTime;
                standardOperationTimeItem.SotValue = sotValue;

                _context.Update(standardOperationTimeItem);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateSotValueOfSection(int sotId, int sectionId, double totalSotValue)
        {
            var result = false;

            var sectionStandardOperationTime = await _context.SectionStandardOperationTimes
                    .Where(m => m.StandardOperationTimeId == sotId && m.SectionId == sectionId)
                    .FirstOrDefaultAsync();

            if (sectionStandardOperationTime != null)
            {
                sectionStandardOperationTime.SotValue = totalSotValue;
                _context.Update(sectionStandardOperationTime);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCalculationStatus(int sotId, int calculationStatusId)
        {
            var result = false;

            var standardOperationTime = await _context.StandardOperationTimes.FirstOrDefaultAsync(m => m.Id == sotId);

            if (standardOperationTime != null)
            {
                standardOperationTime.CalculationStatusId = calculationStatusId;
                _context.Update(standardOperationTime);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateValidationStatus(int sotId, int validationStatusId)
        {
            var result = false;

            var standardOperationTime = await _context.StandardOperationTimes.FirstOrDefaultAsync(m => m.Id == sotId);

            if (standardOperationTime != null)
            {
                standardOperationTime.ValidationStatusId = validationStatusId;
                _context.Update(standardOperationTime);
                int count = await _context.SaveChangesAsync();
                if (count == 1)
                {
                    result = true;
                }
            }

            return Json(result);
        }

    }
}
