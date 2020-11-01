using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.Skills;
using Dynamo.Model.Skills.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;
using jQueryDatatableServerSideNetCore22.Models.AuxiliaryModels;
using jQueryDatatableServerSideNetCore22.Extensions;

namespace DecathlonDynamoErpApp.Controllers.Skills
{
    [Authorize]
    public class SkillMatricesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SkillMatricesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SkillMatrices
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
            var operators = _context.Operators.Include(o => o.Line).Include(o => o.Section).Where(m => m.Section.FactoryId == loggedInUser.FactoryId);
            return View(await operators.ToListAsync());
        }

        // GET: SkillMatrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillMatrix = await _context.SkillMatrixs
                .Include(s => s.Operation)
                .Include(s => s.Operator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillMatrix == null)
            {
                return NotFound();
            }

            return View(skillMatrix);
        }

        // GET: SkillMatrices/Create
        public IActionResult Create()
        {
            ViewData["OperationId"] = new SelectList(_context.Operations, "Id", "Name");
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name");
            return View();
        }

        // POST: SkillMatrices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OperationId,OperatorId,StandardSotInSecond,StandardRft,SotScore,RftScore,UpdatedOn")] SkillMatrix skillMatrix)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skillMatrix);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperationId"] = new SelectList(_context.Operations, "Id", "Name", skillMatrix.OperationId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name", skillMatrix.OperatorId);
            return View(skillMatrix);
        }

        // GET: SkillMatrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillMatrix = await _context.SkillMatrixs.FindAsync(id);
            if (skillMatrix == null)
            {
                return NotFound();
            }
            ViewData["OperationId"] = new SelectList(_context.Operations, "Id", "Name", skillMatrix.OperationId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name", skillMatrix.OperatorId);
            return View(skillMatrix);
        }

        // POST: SkillMatrices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OperationId,OperatorId,StandardSotInSecond,StandardRft,SotScore,RftScore,UpdatedOn")] SkillMatrix skillMatrix)
        {
            if (id != skillMatrix.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skillMatrix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillMatrixExists(skillMatrix.Id))
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
            ViewData["OperationId"] = new SelectList(_context.Operations, "Id", "Name", skillMatrix.OperationId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name", skillMatrix.OperatorId);
            return View(skillMatrix);
        }

        // GET: SkillMatrices/Delete/5  
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var skillMatrix = await _context.SkillMatrixs
        //        .Include(s => s.Operation)
        //        .Include(s => s.Operator)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (skillMatrix == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(skillMatrix);
        //}

        // POST: SkillMatrices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var skillMatrix = await _context.SkillMatrixs.FindAsync(id);
        //    _context.SkillMatrixs.Remove(skillMatrix);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool SkillMatrixExists(int id)
        {
            return _context.SkillMatrixs.Any(e => e.Id == id);
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
        public async Task<IActionResult> OperatorSkills(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @operator = await _context.Operators
                .Include(m => m.Section)
                .Include(m => m.Line)
                .FirstOrDefaultAsync(m => m.Id == id);

            var skillMatrices = await _context.SkillMatrixs
                .Include(s => s.Operation)
                .Include(s => s.Operator)
                .Where(s => s.OperatorId == id)
                .ToListAsync();

            if (@operator == null)
            {
                return NotFound();
            }

            ViewData["Operator"] = @operator;

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var skillMatrixRanges = await _context.SkillMatrixRanges.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();
            ViewData["SkillMatrixRanges"] = skillMatrixRanges;
            ViewData["Sections"] = await _context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId).ToListAsync();

            return View(skillMatrices);
        }

        [HttpPost]
        public async Task<JsonResult> Save(string data)
        {
            var skillMatrixViewModel = JsonConvert.DeserializeObject<SkillMatrixViewModel>(data);

            var skillMatrix = new SkillMatrix();
            skillMatrix.OperationId = skillMatrixViewModel.operationId;
            skillMatrix.OperatorId = skillMatrixViewModel.operatorId;
            skillMatrix.StandardRft = skillMatrixViewModel.standardRft;
            skillMatrix.StandardSotInSecond = skillMatrixViewModel.standardSot;
            skillMatrix.RftScore = skillMatrixViewModel.scoreRft;
            skillMatrix.SotScore = skillMatrixViewModel.scoreSot;
            skillMatrix.TargetMonth = skillMatrixViewModel.targetMonth;
            skillMatrix.TargetGrade = skillMatrixViewModel.targetGrade;
            skillMatrix.UpdatedOn = DateTime.Now;
            if (skillMatrixViewModel.rowId != -1)
            {
                skillMatrix.Id = skillMatrixViewModel.rowId;
                _context.SkillMatrixs.Update(skillMatrix);
                await _context.SaveChangesAsync();
                return new JsonResult(skillMatrix.Id);
            }
            else
            {
                _context.SkillMatrixs.Add(skillMatrix);
                await _context.SaveChangesAsync();
                return new JsonResult(skillMatrix.Id);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveAll(string skillMatrices)
        {
            var error = false;
            var count = 0;
            var skillMatrixViewModels = JsonConvert.DeserializeObject<List<SkillMatrixViewModel>>(skillMatrices);
            foreach (var skillMatrixViewModel in skillMatrixViewModels)
            {
                var skillMatrix = new SkillMatrix();
                skillMatrix.OperationId = skillMatrixViewModel.operationId;
                skillMatrix.OperatorId = skillMatrixViewModel.operatorId;
                skillMatrix.StandardRft = skillMatrixViewModel.standardRft;
                skillMatrix.StandardSotInSecond = skillMatrixViewModel.standardSot;
                skillMatrix.RftScore = skillMatrixViewModel.scoreRft;
                skillMatrix.SotScore = skillMatrixViewModel.scoreSot;
                skillMatrix.TargetMonth = skillMatrixViewModel.targetMonth;
                skillMatrix.TargetGrade = skillMatrixViewModel.targetGrade;
                skillMatrix.UpdatedOn = DateTime.Now;

                if (skillMatrixViewModel.rowId != -1)
                {
                    skillMatrix.Id = skillMatrixViewModel.rowId;
                    _context.SkillMatrixs.Update(skillMatrix);
                    try
                    {
                        await _context.SaveChangesAsync();
                        count++;
                    }
                    catch
                    {
                        error = true;
                    }
                }
                else
                {
                    _context.SkillMatrixs.Add(skillMatrix);
                    try
                    {
                        await _context.SaveChangesAsync();
                        count++;
                    }
                    catch
                    {
                        error = true;
                    }
                }
            }
            if (error == false)
            {
                return new JsonResult(count);
            }
            else
            {
                return new JsonResult(false);
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
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var skillMatrix = await _context.SkillMatrixs.FindAsync(id);
                _context.SkillMatrixs.Remove(skillMatrix);
                await _context.SaveChangesAsync();
                return new JsonResult(true);
            }
            catch
            {
                return new JsonResult(false);
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
            List<SkillMatrix> skillMatrices = new List<SkillMatrix>();

            //here
            foreach (var item in result)
            {
                SkillMatrix tempSkillMatrix = new SkillMatrix
                {
                    Id = item.Id,
                    LineLineNumber = item.Line.LineNumber,
                    SectionName = item.Section.Name,
                    OperatorName = item.Name,
                    IdCardNumber = item.IdCardNumber,
                    JoiningDate = item.JoiningDate,
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber
                };
                skillMatrices.Add(tempSkillMatrix);
            }

            //here
            if (!string.IsNullOrEmpty(searchBy))
            {
                skillMatrices = skillMatrices.Where(r => r.LineLineNumber != null && r.LineLineNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.SectionName != null && r.SectionName.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.OperatorName != null && r.OperatorName.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.IdCardNumber != null && r.IdCardNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.JoiningDate != null && r.JoiningDate.ToString("dd-MM-yyyy").Contains(searchBy.ToString()) ||
                                           r.Address != null && r.Address.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.PhoneNumber != null && r.PhoneNumber.ToUpper().Contains(searchBy.ToUpper()))
                    .ToList();
            }

            //here
            skillMatrices = orderAscendingDirection ? skillMatrices.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Asc).ToList() : skillMatrices.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Desc).ToList();

            var filteredResultsCount = result.Count();
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
                    data = skillMatrices
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
                    data = skillMatrices
                  .Skip(dtParameters.Start)
                  .Take(dtParameters.Length)
                  .ToList()
                });
            }
        }
    }
}
