using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;
using jQueryDatatableServerSideNetCore22.Models.AuxiliaryModels;
using System;
using System.Collections.Generic;
using jQueryDatatableServerSideNetCore22.Extensions;

namespace DecathlonDynamoErpApp.Controllers.Operations
{
    [Authorize]
    public class OperatorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OperatorsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Operators
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
            ViewData["ControllerName"] = "Operators";
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var operators = _context.Operators.Include(o => o.Line).Include(o => o.Section).Where(m => m.Section.FactoryId == loggedInUser.FactoryId);
            return View(await operators.ToListAsync());
        }

        // GET: Operators/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var @operator = await _context.Operators
        //        .Include(o => o.Line)
        //        .Include(o => o.Section)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (@operator == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@operator);
        //}

        // GET: Operators/Create
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Create()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["LineId"] = new SelectList(_context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId), "Id", "LineNumber");
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name");
            return View();
        }

        // POST: Operators/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Create([Bind("Id,LineId,SectionId,Name,IdCardNumber,JoiningDate,Address,PhoneNumber")] Operator @operator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@operator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["LineId"] = new SelectList(_context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId), "Id", "LineNumber", @operator.LineId);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", @operator.SectionId);
            return View(@operator);
        }

        // GET: Operators/Edit/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @operator = await _context.Operators.FirstOrDefaultAsync(m => m.Id == id);
            if (@operator == null)
            {
                return NotFound();
            }

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["LineId"] = new SelectList(_context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId), "Id", "LineNumber", @operator.LineId);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", @operator.SectionId);
            return View(@operator);
        }

        // POST: Operators/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LineId,SectionId,Name,IdCardNumber,JoiningDate,Address,PhoneNumber")] Operator @operator)
        {
            if (id != @operator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@operator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperatorExists(@operator.Id))
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
            ViewData["LineId"] = new SelectList(_context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId), "Id", "LineNumber", @operator.LineId);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", @operator.SectionId);
            return View(@operator);
        }

        // GET: Operators/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @operator = await _context.Operators
                .Include(o => o.Line)
                .Include(o => o.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@operator == null)
            {
                return NotFound();
            }

            return View(@operator);
        }

        // POST: Operators/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @operator = await _context.Operators.FindAsync(id);
            _context.Operators.Remove(@operator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperatorExists(int id)
        {
            return _context.Operators.Any(e => e.Id == id);
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
            var result = await _context.Operators.Include(o => o.Line).Include(o => o.Section).Where(m => m.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();


            //here
            List<Operator> operators = new List<Operator>();
            foreach (var item in result)
            {
                Operator tempOperator = new Operator();
                tempOperator.Id = item.Id;
                tempOperator.LineNumber = item.Line.LineNumber;
                tempOperator.SectionName = item.Section.Name;
                tempOperator.Name = item.Name;
                tempOperator.IdCardNumber = item.IdCardNumber;
                tempOperator.JoiningDate = item.JoiningDate;
                tempOperator.Address = item.Address;
                tempOperator.PhoneNumber = item.PhoneNumber;
                operators.Add(tempOperator);
            }

            //here
            if (!string.IsNullOrEmpty(searchBy))
            {
                operators = operators.Where(r =>

                r.LineNumber != null && r.LineNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                r.SectionName != null && r.SectionName.ToUpper().Contains(searchBy.ToUpper()) ||
                r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                r.IdCardNumber != null && r.IdCardNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                r.JoiningDate != null && r.JoiningDate.ToString("dd-MM-yyyy").Contains(searchBy.ToString()) ||
                r.Address != null && r.Address.ToUpper().Contains(searchBy.ToUpper()) ||
                r.PhoneNumber != null && r.PhoneNumber.ToUpper().Contains(searchBy.ToUpper())

                ).ToList();
            }

            //here
            operators = orderAscendingDirection ? operators.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Asc).ToList() : operators.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Desc).ToList();

            //here
            var filteredResultsCount = operators.Count();

            //here
            var totalResultsCount = await _context.Operators.Include(o => o.Line).Include(o => o.Section).Where(m => m.Section.FactoryId == loggedInUser.FactoryId).CountAsync();
            if (dtParameters.Length == -1)
            {
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    //here
                    data = operators
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
                    data = operators
                  .Skip(dtParameters.Start)
                  .Take(dtParameters.Length)
                  .ToList()
                });
            }
        }
    }
}
