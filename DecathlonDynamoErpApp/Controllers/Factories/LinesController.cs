using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;
using jQueryDatatableServerSideNetCore22.Models.AuxiliaryModels;
using System;
using jQueryDatatableServerSideNetCore22.Extensions;
using System.Collections.Generic;

namespace DecathlonDynamoErpApp.Controllers.Factories
{
    [Authorize]
    public class LinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LinesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Lines
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

        // GET: Line Incentives
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public IActionResult LineIncentiveIndex()
        {
            return View();
        }

        // GET: Lines/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var line = await _context.Lines
        //        .Include(l => l.Section)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (line == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(line);
        //}

        // GET: Lines/Create
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Create()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name");
            return View();
        }

        // POST: Lines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Create([Bind("Id,SectionId,LineNumber")] Line line)
        {
            if (ModelState.IsValid)
            {
                _context.Add(line);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", line.SectionId);
            return View(line);
        }

        // GET: Lines/Edit/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var line = await _context.Lines.FirstOrDefaultAsync(m => m.Id == id);
            if (line == null)
            {
                return NotFound();
            }

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", line.SectionId);
            return View(line);
        }

        // POST: Lines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionId,LineNumber")] Line line)
        {
            if (id != line.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(line);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineExists(line.Id))
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
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", line.SectionId);
            return View(line);
        }

        // GET: Lines/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var line = await _context.Lines
                .Include(l => l.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (line == null)
            {
                return NotFound();
            }

            return View(line);
        }

        // POST: Lines/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var line = await _context.Lines.FindAsync(id);
            _context.Lines.Remove(line);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineExists(int id)
        {
            return _context.Lines.Any(e => e.Id == id);
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
            var result = await _context.Lines.Include(l => l.Section).Where(m => m.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();


            //here
            List<Line> lines = new List<Line>();
            foreach (var item in result)
            {
                Line tempLine = new Line();
                tempLine.Id = item.Id;
                tempLine.SectionName = item.Section.Name;
                tempLine.LineNumber = item.LineNumber;
                lines.Add(tempLine);
            }

            //here
            if (!string.IsNullOrEmpty(searchBy))
            {
                lines = lines.Where(r =>

                r.SectionName != null && r.SectionName.ToUpper().Contains(searchBy.ToUpper()) ||
                r.LineNumber != null && r.LineNumber.ToUpper().Contains(searchBy.ToUpper())

                ).ToList();
            }

            //here
            lines = orderAscendingDirection ? lines.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Asc).ToList() : lines.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Desc).ToList();

            //here
            var filteredResultsCount = lines.Count();
            //here
            var totalResultsCount = await _context.Lines.Include(l => l.Section).Where(m => m.Section.FactoryId == loggedInUser.FactoryId).CountAsync();
            if (dtParameters.Length == -1)
            {
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    //here
                    data = lines
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
                    //here
                    data = lines
                  .Skip(dtParameters.Start)
                  .Take(dtParameters.Length)
                  .ToList()
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> LoadTable2([FromBody]DTParameters dtParameters)
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
            var result = await _context.Lines
                .Include(l => l.Section)
                .Include(m => m.LineIncentives)
                .Where(m => m.Section.FactoryId == loggedInUser.FactoryId)
                .ToListAsync();

            //here
            List<Line> lines = new List<Line>();

            //here
            foreach (var line in result)
            {
                var total = 0.0;
                foreach (var lineIncentive in line.LineIncentives)
                {
                    total += (lineIncentive.Total != null) ? Convert.ToDouble(lineIncentive.Total) : 0;
                }

                Line tempLine = new Line
                {
                    Id = line.Id,
                    SectionName = line.Section.Name,
                    LineNumber = line.LineNumber,
                    LineIncentiveTotal = total
                };

                lines.Add(tempLine);
            }

            //here
            if (!string.IsNullOrEmpty(searchBy))
            {
                lines = lines.Where(r => r.SectionName != null && r.SectionName.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.LineNumber != null && r.LineNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.LineIncentiveTotal.ToString().Contains(searchBy.ToUpper())
                                           ).ToList();
            }

            //here
            lines = orderAscendingDirection ? lines.AsQueryable()
                .OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : lines.AsQueryable()
                .OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();

            var filteredResultsCount = result.Count();
            //here
            var totalResultsCount = await _context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId).CountAsync();
            if (dtParameters.Length == -1)
            {
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    //here
                    data = lines
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
                    //here
                    data = lines
                  .Skip(dtParameters.Start)
                  .Take(dtParameters.Length)
                  .ToList()
                });
            }
        }
    }
}
