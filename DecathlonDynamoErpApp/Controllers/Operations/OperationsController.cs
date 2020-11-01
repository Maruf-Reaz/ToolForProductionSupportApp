using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.Operations;
using Microsoft.AspNetCore.Authorization;
using Dynamo.Model.Machines;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;
using jQueryDatatableServerSideNetCore22.Models.AuxiliaryModels;
using jQueryDatatableServerSideNetCore22.Extensions;
using System;
using System.Collections.Generic;

namespace DecathlonDynamoErpApp.Controllers.Operations
{
    [Authorize]
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OperationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Operations
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
            var operations = _context.Operations.Where(m => m.FactoryId == loggedInUser.FactoryId).Include(o => o.Machine).Include(o => o.Section);
            return View(await operations.ToListAsync());
        }

        // GET: Operations/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var operation = await _context.Operations
        //        .Include(o => o.Machine)
        //        .Include(o => o.Section)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (operation == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(operation);
        //}

        // GET: Operations/Create
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Create()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["MachineId"] = new SelectList(_context.Machines.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name");
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name");
            return View();
        }

        // POST: Operations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Create([Bind("Id,SectionId,MachineId,Name")] Operation operation)
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (ModelState.IsValid)
            {
                operation.FactoryId = loggedInUser.FactoryId;
                _context.Add(operation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachineId"] = new SelectList(_context.Machines.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", operation.MachineId);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", operation.SectionId);
            return View(operation);
        }

        // GET: Operations/Edit/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations.FirstOrDefaultAsync(m => m.Id == id);
            if (operation == null)
            {
                return NotFound();
            }

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["MachineId"] = new SelectList(_context.Machines.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", operation.MachineId);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", operation.SectionId);
            return View(operation);
        }

        // POST: Operations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FactoryId,SectionId,MachineId,Name")] Operation operation)
        {
            if (id != operation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationExists(operation.Id))
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
            ViewData["MachineId"] = new SelectList(_context.Machines.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", operation.MachineId);
            ViewData["SectionId"] = new SelectList(_context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", operation.SectionId);
            return View(operation);
        }

        // GET: Operations/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations
                .Include(o => o.Machine)
                .Include(o => o.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // POST: Operations/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operation = await _context.Operations.FindAsync(id);
            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationExists(int id)
        {
            return _context.Operations.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<JsonResult> GetAllBySection(int? id)
        {
            if (id == null)
            {
                return new JsonResult(false);
            }

            var operations = await _context.Operations.Where(m => m.SectionId == id).ToListAsync();
            if (operations == null)
            {
                return new JsonResult(false);
            }

            return new JsonResult(operations);
        }

        // POST: Operations/GetMachineDetails/5
        [HttpPost]
        public async Task<JsonResult> GetMachineDetails(int id)
        {
            var operation = await _context.Operations.Include(m => m.Machine).FirstOrDefaultAsync(m => m.Id == id);
            var machine = new Machine { Name = operation.Machine.Name, Total = operation.Machine.Total };
            return Json(machine);
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
                // in this example we just default sort on the 1st column
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

                //orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";

                // orderCriteria = "Id";


            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var result = await _context.Operations.Where(m => m.FactoryId == loggedInUser.FactoryId).Include(o => o.Machine).Include(o => o.Section).ToListAsync();


            //here
            List<Operation> operations = new List<Operation>();

            foreach (var item in result)
            {
                Operation tempOperation = new Operation();
                tempOperation.Id = item.Id;
                tempOperation.Name = item.Name;
                tempOperation.SectionName = item.Section.Name;
                tempOperation.MachineName = item.Machine.Name;
                operations.Add(tempOperation);
            }

            //here
            if (!string.IsNullOrEmpty(searchBy))
            {
                operations = operations.Where(r =>

                r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                r.SectionName != null && r.SectionName.ToUpper().Contains(searchBy.ToUpper()) ||
                r.MachineName != null && r.MachineName.ToUpper().Contains(searchBy.ToUpper())

                ).ToList();
            }

            operations = orderAscendingDirection ? operations.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Asc).ToList() : operations.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Desc).ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = operations.Count();
            var totalResultsCount = await _context.Operations.Where(m => m.FactoryId == loggedInUser.FactoryId).Include(o => o.Machine).Include(o => o.Section).CountAsync();
            if (dtParameters.Length == -1)
            {
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = operations
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
                    data = operations
                  .Skip(dtParameters.Start)
                  .Take(dtParameters.Length)
                  .ToList()
                });
            }
        }
    }
}
