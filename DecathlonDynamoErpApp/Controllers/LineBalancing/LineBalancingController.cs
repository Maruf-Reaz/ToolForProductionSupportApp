using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamo.Data;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Factories;
using Dynamo.Model.LineBalancing;
using Dynamo.Model.LineBalancing.ViewModels;
using Dynamo.Model.Operations.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DecathlonDynamoErpApp.Controllers.LineBalancing
{
    [Authorize]
    public class LineBalancingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public LineBalancingController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin,KSIUserOfLineBalancing,YSSUserOfLineBalancing,MAFUserOfLineBalancing,RFLUserOfLineBalancing,APEXUserOfLineBalancing,EDISONUserOfLineBalancing,KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> Index(int? id)
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (id == null)
            {
                return NotFound();
            }

            var ParticularLineBalancing = await _context.ParticularLineBalancings
                .Include(m => m.StandardOperationTime).ThenInclude(d => d.SotModel)
                .Where(m => m.Id == id).FirstOrDefaultAsync();

            var line = await _context.Lines.Include(m => m.Section)
                .Where(m => m.Id == ParticularLineBalancing.LineId).FirstOrDefaultAsync();

            if (line == null)
            {
                return NotFound();
            }

            var operators = await _context.Operators.Include(m => m.Section).Include(m => m.Line).Where(m => m.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();
            var operations = await _context.Operations.Include(m => m.Machine).Where(m => m.SectionId == line.SectionId && m.FactoryId == loggedInUser.FactoryId).ToListAsync();
            var lineBalancingStates = await _context.LineBalancingStates
                .Where(m => m.ParticularLineBalancingId == id)
                .ToListAsync();

            List<LowOutputViewModel> lowOutputViewModels = new List<LowOutputViewModel>();
            foreach (var item in lineBalancingStates)
            {
                LowOutputViewModel lowOutputViewModel = new LowOutputViewModel();
                if (item.Output1 != null)
                {
                    lowOutputViewModel.TotalOutPut += (double)item.Output1;
                }

                if (item.Output2 != null)
                {
                    lowOutputViewModel.TotalOutPut += (double)item.Output2;
                }
                if (item.Output3 != null)
                {
                    lowOutputViewModel.TotalOutPut += (double)item.Output3;
                }
                if (item.Output4 != null)
                {
                    lowOutputViewModel.TotalOutPut += (double)item.Output4;
                }

                lowOutputViewModel.StateId = item.Id;
                lowOutputViewModels.Add(lowOutputViewModel);
            }
            List<LowOutputViewModel> tempList = lowOutputViewModels.OrderBy(m => m.TotalOutPut).ToList();
            List<LowOutputViewModel> sendList = tempList.Take(3).ToList();

            ViewData["LowOutputViewModels"] = sendList;
            ViewData["Operators"] = operators;

            ViewData["Operations"] = operations;
            ViewData["Line"] = line;
            ViewData["ParticularLineBalancing"] = ParticularLineBalancing;

            return View(lineBalancingStates);
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin,KSIUserOfLineBalancing,YSSUserOfLineBalancing,MAFUserOfLineBalancing,RFLUserOfLineBalancing,APEXUserOfLineBalancing,EDISONUserOfLineBalancing")]
        public async Task<IActionResult> CreateLineBalancing()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var Sots = _context.StandardOperationTimes
                .Include(m => m.SotModel)
                .Where(m => m.FactoryId == loggedInUser.FactoryId)
                .ToList();
            ViewData["StandardOperationTimes"] = Sots;

            var linesToCreateParticularLineBalanging = new List<Line>();
            var lines = await _context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();
            var particularLineBalancings = await _context.ParticularLineBalancings.Where(m => m.Line.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();
            foreach (var line in lines)
            {
                bool isCreated = false;
                foreach (var particularLineBalancing in particularLineBalancings)
                {
                    if (line.Id == particularLineBalancing.LineId)
                    {
                        isCreated = true;
                    }
                }
                if (isCreated == false)
                {
                    linesToCreateParticularLineBalanging.Add(line);
                }
            }

            //ViewData["LineId"] = new SelectList(_context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId), "Id", "LineNumber");
            ViewData["LineId"] = new SelectList(linesToCreateParticularLineBalanging, "Id", "LineNumber");
            return View();
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin,KSIUserOfLineBalancing,YSSUserOfLineBalancing,MAFUserOfLineBalancing,RFLUserOfLineBalancing,APEXUserOfLineBalancing,EDISONUserOfLineBalancing")]
        [HttpPost]
        public async Task<IActionResult> CreateLineBalancing(ParticularLineBalancing particularLineBalancing)
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (ModelState.IsValid)
            {
                ParticularLineBalancing tempLineBalancing = new ParticularLineBalancing();

                tempLineBalancing.LineId = particularLineBalancing.LineId;
                tempLineBalancing.StandardOperationTimeId = particularLineBalancing.StandardOperationTimeId;
                tempLineBalancing.Who = particularLineBalancing.Who;

                _context.Add(tempLineBalancing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AllLines));
            }
            var Sots = _context.StandardOperationTimes
               .Include(m => m.SotModel)
               .Where(m => m.FactoryId == loggedInUser.FactoryId)
               .ToList();

            var linesToCreateParticularLineBalanging = new List<Line>();
            var lines = await _context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();
            var particularLineBalancings = await _context.ParticularLineBalancings.Where(m => m.Line.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();
            foreach (var line in lines)
            {
                bool isCreated = false;
                foreach (var tempParticularLineBalancing in particularLineBalancings)
                {
                    if (line.Id == tempParticularLineBalancing.LineId)
                    {
                        isCreated = true;
                    }
                }
                if (isCreated == false)
                {
                    linesToCreateParticularLineBalanging.Add(line);
                }
            }

            ViewData["LineId"] = new SelectList(linesToCreateParticularLineBalanging, "Id", "LineNumber");
            ViewData["StandardOperationTimes"] = Sots;

            return View(particularLineBalancing);
        }

        // GET: Factories/Edit/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin,KSIUserOfLineBalancing,YSSUserOfLineBalancing,MAFUserOfLineBalancing,RFLUserOfLineBalancing,APEXUserOfLineBalancing,EDISONUserOfLineBalancing")]
        public async Task<IActionResult> EditLineBalancing(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var particularLineBalancing = await _context.ParticularLineBalancings
                .Include(m => m.Line)
                .Include(m => m.StandardOperationTime)
                .ThenInclude(m => m.SotModel)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (particularLineBalancing == null)
            {
                return NotFound();
            }
            ViewData["ParticularLineBalancing"] = particularLineBalancing;

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var Sots = _context.StandardOperationTimes
                .Include(m => m.SotModel)
                .Where(m => m.FactoryId == loggedInUser.FactoryId)
                .ToList();
            ViewData["StandardOperationTimes"] = Sots;

            return View(particularLineBalancing);
        }

        // POST: Factories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin,KSIUserOfLineBalancing,YSSUserOfLineBalancing,MAFUserOfLineBalancing,RFLUserOfLineBalancing,APEXUserOfLineBalancing,EDISONUserOfLineBalancing")]
        public async Task<IActionResult> EditLineBalancing(int id, ParticularLineBalancing particularLineBalancing)
        {
            if (id != particularLineBalancing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var tempLineBalancing = await _context.ParticularLineBalancings
                    .Where(m => m.Id == id)
                    .FirstOrDefaultAsync();
                tempLineBalancing.LineId = particularLineBalancing.LineId;
                tempLineBalancing.StandardOperationTimeId = particularLineBalancing.StandardOperationTimeId;
                tempLineBalancing.Who = particularLineBalancing.Who;
                tempLineBalancing.ActualLineBalancingRatio = particularLineBalancing.ActualLineBalancingRatio;

                _context.Update(tempLineBalancing);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(AllLines));
            }

            ViewData["ParticularLineBalancing"] = particularLineBalancing;

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var Sots = _context.StandardOperationTimes
                .Include(m => m.SotModel)
                .Where(m => m.FactoryId == loggedInUser.FactoryId)
                .ToList();
            ViewData["StandardOperationTimes"] = Sots;

            return View(particularLineBalancing);
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin,KSIUserOfLineBalancing,YSSUserOfLineBalancing,MAFUserOfLineBalancing,RFLUserOfLineBalancing,APEXUserOfLineBalancing,EDISONUserOfLineBalancing,KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> Graph(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var particularLineBalancing = await _context.ParticularLineBalancings
                .Include(m => m.StandardOperationTime)
             .Where(m => m.Id == id).FirstOrDefaultAsync();

            var line = _context.Lines.Find(particularLineBalancing.LineId);

            if (line == null)
            {
                return NotFound();
            }
            var lineBalancingStates = await _context.LineBalancingStates.Where(m => m.ParticularLineBalancingId == id).ToListAsync();
            if (lineBalancingStates.Count == 0)
            {
                return NotFound();
            }

            List<LineBalancingStateViewModel> lineBalancingStateViewModels = GetLineBalancingStateViewModels(lineBalancingStates);
            BottleNeckViewModel bottleNeckViewModel = GetBottleNeckViewModel(lineBalancingStateViewModels);
            //double dailyWorkHours = 8;
            //double allowance = 20;
            //double firstBottleNeck = GetFirstBottleNeck(lineBalancingStateViewModels);
            double targetLbRatio = 85;
            string firstBottleNeckOperationName = bottleNeckViewModel.FirstBottleNeckName;
            double? firstBottleNeckValue = bottleNeckViewModel.FirstBottleNeckValue;
            string secondBottleNeckOperationName = bottleNeckViewModel.SecondBottleNeckName;
            double? secondBottleNeckValue = bottleNeckViewModel.SecondBottleNeckValue;
            double totalNumberOfOperators = GetTotalNumberOfOperators(lineBalancingStateViewModels);
            double totalProcessingTime = GetTotalCycleTime(lineBalancingStateViewModels);
            double bpt = GetBpt(totalProcessingTime, totalNumberOfOperators);
            double ucl = bpt / 0.85;
            double lcl = 2 * bpt - ucl;
            double actualLbRatio = bpt / (double)firstBottleNeckValue;
            double actualOutput = GetDailyOutput(totalNumberOfOperators, totalProcessingTime, actualLbRatio);
            double targetOutput = GetDailyOutput(totalNumberOfOperators, totalProcessingTime, targetLbRatio / 100);

            particularLineBalancing.ActualLineBalancingRatio = actualLbRatio;
            _context.Update(particularLineBalancing);
            await _context.SaveChangesAsync();

            var sotValue = await _context.SectionStandardOperationTimes
                .Where(m => m.StandardOperationTimeId == particularLineBalancing.StandardOperationTimeId && m.SectionId == line.SectionId)
                .FirstOrDefaultAsync();

            //ViewData["DailyWorkHours"] = dailyWorkHours;
            //ViewData["Allowance"] = allowance;
            ViewData["TotalNumberOfOperators"] = totalNumberOfOperators;
            ViewData["TotalProcessingTime"] = totalProcessingTime;
            ViewData["FirstBottleNeckValue"] = firstBottleNeckValue;
            ViewData["FirstBottleNeckOperationName"] = firstBottleNeckOperationName;
            ViewData["SecondBottleNeckValue"] = secondBottleNeckValue;
            ViewData["SecondBottleNeckOperationName"] = secondBottleNeckOperationName;
            ViewData["Bpt"] = bpt;
            ViewData["ActualOutput"] = actualOutput;
            ViewData["TargetOutput"] = targetOutput;
            ViewData["TargetLbRatio"] = (targetLbRatio).ToString("#.##");
            ViewData["ActualLbRatio"] = (actualLbRatio * 100).ToString("#.##");
            ViewData["Ucl"] = ucl;
            ViewData["Lcl"] = lcl;
            ViewData["Line"] = line;
            ViewData["TotalSotValue"] = Math.Round(sotValue.SotValue, 3);

            return View(lineBalancingStateViewModels);
        }

        public JsonResult GetGraphData(int? id)
        {
            var lineBalancingStates = _context.LineBalancingStates.Where(m => m.ParticularLineBalancingId == id).ToList();
            List<LineBalancingStateViewModel> lineBalancingStateViewModels = GetLineBalancingStateViewModels(lineBalancingStates);
            return new JsonResult(lineBalancingStateViewModels);
        }

        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin,KSIUserOfLineBalancing,YSSUserOfLineBalancing,MAFUserOfLineBalancing,RFLUserOfLineBalancing,APEXUserOfLineBalancing,EDISONUserOfLineBalancing,KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> AllLines()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var lineBalancings = _context.ParticularLineBalancings
                .Include(l => l.Line).ThenInclude(l => l.Section)
                .Include(l => l.StandardOperationTime).ThenInclude(l => l.SotModel)
                .Where(m => m.Line.Section.FactoryId == loggedInUser.FactoryId);

            return View(await lineBalancings.ToListAsync());
        }

        public async Task<JsonResult> GetAllOperations(int lineId)
        {
            var newLine = await _context.Lines.FirstOrDefaultAsync(m => m.ParticularLineBalancings.Id == lineId);

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var operations = await _context.Operations.Where(m => m.SectionId == newLine.SectionId && m.FactoryId == loggedInUser.FactoryId).ToListAsync();

            return new JsonResult(operations);
        }

        public async Task<JsonResult> GetAllOperators()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var operators = await _context.Operators.Where(m => m.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();

            return new JsonResult(operators);
        }

        public async Task<JsonResult> GetMachineByOperation(int id)
        {
            var operation = await _context.Operations.Include(m => m.Machine).Where(m => m.Id == id).FirstOrDefaultAsync();
            var machine = operation.Machine;

            return new JsonResult(machine);
        }

        public JsonResult GetAllMachines()
        {
            return new JsonResult(_context.Machines.ToList());
        }

        [HttpPost]
        public async Task<JsonResult> Save(string data)
        {
            var lineBalancingViewModels = JsonConvert.DeserializeObject<LineBalancingViewModel>(data);
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var lineBalancingState = new LineBalancingState();
            lineBalancingState.ParticularLineBalancingId = lineBalancingViewModels.ParticularLineBalancingId;
            lineBalancingState.OperationName = lineBalancingViewModels.OperationName;
            lineBalancingState.OperationId = lineBalancingViewModels.OperationId;
            lineBalancingState.MachineId = lineBalancingViewModels.MachineId;

            lineBalancingState.OperatorId1 = lineBalancingViewModels.OperatorId1;
            lineBalancingState.OperatorId2 = lineBalancingViewModels.OperatorId2;
            lineBalancingState.OperatorId3 = lineBalancingViewModels.OperatorId3;
            lineBalancingState.OperatorId4 = lineBalancingViewModels.OperatorId4;

            lineBalancingState.OperatorNo1 = lineBalancingViewModels.OperatorNo1;
            lineBalancingState.OperatorNo2 = lineBalancingViewModels.OperatorNo2;
            lineBalancingState.OperatorNo3 = lineBalancingViewModels.OperatorNo3;
            lineBalancingState.OperatorNo4 = lineBalancingViewModels.OperatorNo4;

            lineBalancingState.CycleTime1 = lineBalancingViewModels.CycleTime1;
            lineBalancingState.CycleTime2 = lineBalancingViewModels.CycleTime2;
            lineBalancingState.CycleTime3 = lineBalancingViewModels.CycleTime3;
            lineBalancingState.CycleTime4 = lineBalancingViewModels.CycleTime4;

            lineBalancingState.AllocatedTime1 = lineBalancingViewModels.AllocatedTime1;
            lineBalancingState.AllocatedTime2 = lineBalancingViewModels.AllocatedTime2;
            lineBalancingState.AllocatedTime3 = lineBalancingViewModels.AllocatedTime3;
            lineBalancingState.AllocatedTime4 = lineBalancingViewModels.AllocatedTime4;

            lineBalancingState.Output1 = lineBalancingViewModels.Output1;
            lineBalancingState.Output2 = lineBalancingViewModels.Output2;
            lineBalancingState.Output3 = lineBalancingViewModels.Output3;
            lineBalancingState.Output4 = lineBalancingViewModels.Output4;

            if (lineBalancingViewModels.RowId != -1)
            {
                lineBalancingState.Id = lineBalancingViewModels.RowId;
                _context.LineBalancingStates.Update(lineBalancingState);
                await _context.SaveChangesAsync();
                var particularLine = await _context.ParticularLineBalancings.Where(m => m.Id == lineBalancingState.ParticularLineBalancingId).FirstOrDefaultAsync();
                particularLine.Who = loggedInUser.UserName;
                _context.ParticularLineBalancings.Update(particularLine);
                await _context.SaveChangesAsync();
                return new JsonResult(lineBalancingState.Id);
            }
            else
            {
                _context.LineBalancingStates.Add(lineBalancingState);
                await _context.SaveChangesAsync();
                var particularLine = await _context.ParticularLineBalancings.Where(m => m.Id == lineBalancingState.ParticularLineBalancingId).FirstOrDefaultAsync();
                particularLine.Who = loggedInUser.UserName;
                _context.ParticularLineBalancings.Update(particularLine);
                await _context.SaveChangesAsync();
                return new JsonResult(lineBalancingState.Id);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveAll(string lineBalancingStates)
        {
            var error = false;
            var count = 0;
            var lineBalancingViewModels = JsonConvert.DeserializeObject<List<LineBalancingViewModel>>(lineBalancingStates);

            int particularLineId = 0;
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            foreach (var lineBalancingViewModel in lineBalancingViewModels)
            {

                particularLineId = lineBalancingViewModel.ParticularLineBalancingId;
                break;
            }

            foreach (var lineBalancingViewModel in lineBalancingViewModels)
            {
                var lineBalancingState = new LineBalancingState();
                lineBalancingState.ParticularLineBalancingId = lineBalancingViewModel.ParticularLineBalancingId;
                lineBalancingState.OperationName = lineBalancingViewModel.OperationName;
                lineBalancingState.OperationId = lineBalancingViewModel.OperationId;
                lineBalancingState.MachineId = lineBalancingViewModel.MachineId;

                lineBalancingState.OperatorId1 = lineBalancingViewModel.OperatorId1;
                lineBalancingState.OperatorId2 = lineBalancingViewModel.OperatorId2;
                lineBalancingState.OperatorId3 = lineBalancingViewModel.OperatorId3;
                lineBalancingState.OperatorId4 = lineBalancingViewModel.OperatorId4;

                lineBalancingState.OperatorNo1 = lineBalancingViewModel.OperatorNo1;
                lineBalancingState.OperatorNo2 = lineBalancingViewModel.OperatorNo2;
                lineBalancingState.OperatorNo3 = lineBalancingViewModel.OperatorNo3;
                lineBalancingState.OperatorNo4 = lineBalancingViewModel.OperatorNo4;

                lineBalancingState.CycleTime1 = lineBalancingViewModel.CycleTime1;
                lineBalancingState.CycleTime2 = lineBalancingViewModel.CycleTime2;
                lineBalancingState.CycleTime3 = lineBalancingViewModel.CycleTime3;
                lineBalancingState.CycleTime4 = lineBalancingViewModel.CycleTime4;

                lineBalancingState.AllocatedTime1 = lineBalancingViewModel.AllocatedTime1;
                lineBalancingState.AllocatedTime2 = lineBalancingViewModel.AllocatedTime2;
                lineBalancingState.AllocatedTime3 = lineBalancingViewModel.AllocatedTime3;
                lineBalancingState.AllocatedTime4 = lineBalancingViewModel.AllocatedTime4;

                lineBalancingState.Output1 = lineBalancingViewModel.Output1;
                lineBalancingState.Output2 = lineBalancingViewModel.Output2;
                lineBalancingState.Output3 = lineBalancingViewModel.Output3;
                lineBalancingState.Output4 = lineBalancingViewModel.Output4;

                if (lineBalancingViewModel.RowId != -1)
                {
                    lineBalancingState.Id = lineBalancingViewModel.RowId;
                    _context.LineBalancingStates.Update(lineBalancingState);
                    try
                    {
                        await _context.SaveChangesAsync();
                        if (particularLineId != 0)
                        {
                            var particularLine = await _context.ParticularLineBalancings.Where(m => m.Id == particularLineId).FirstOrDefaultAsync();
                            particularLine.Who = loggedInUser.UserName;
                            _context.ParticularLineBalancings.Update(particularLine);
                            await _context.SaveChangesAsync();
                        }

                        count++;
                    }
                    catch
                    {
                        error = true;
                    }
                }
                else
                {
                    _context.LineBalancingStates.Add(lineBalancingState);
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
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var lineBalancingState = await _context.LineBalancingStates.FindAsync(id);
                _context.LineBalancingStates.Remove(lineBalancingState);
                await _context.SaveChangesAsync();
                return new JsonResult(true);
            }
            catch
            {
                return new JsonResult(false);
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAll(int id)
        {
            try
            {
                var lineBalancingState = await _context.LineBalancingStates.Where(m => m.ParticularLineBalancingId == id).ToListAsync();
                _context.LineBalancingStates.RemoveRange(lineBalancingState);
                await _context.SaveChangesAsync();
                return new JsonResult(true);
            }
            catch
            {
                return new JsonResult(false);
            }
        }

        [HttpPost]
        public JsonResult GetOperatorInfo(int id)
        {
            var @operator = _context.Operators
                .Include(m => m.Section)
                .Include(m => m.Line)
                .FirstOrDefault(m => m.Id == id);

            var skillMatrices = _context.SkillMatrixs
                .Include(s => s.Operation)
                .Include(s => s.Operator)
                .Where(s => s.OperatorId == id)
                .ToList();
            OperatorInfoViewModel operatorInfoViewModel = new OperatorInfoViewModel();
            List<OperatorInfoSkillMatrixViewModel> operatorInfoSkillMatrixViewModels = new List<OperatorInfoSkillMatrixViewModel>();

            if (@operator != null)
            {
                operatorInfoViewModel.Name = @operator.Name;
                operatorInfoViewModel.LineNumber = @operator.Line.LineNumber;
                operatorInfoViewModel.SectionName = @operator.Section.Name;
                operatorInfoViewModel.IdCardNumber = @operator.IdCardNumber;
            }
            else
            {
                return new JsonResult(false);
            }
            if (skillMatrices.Count != 0)
            {
                foreach (var skillMatrix in skillMatrices)
                {
                    OperatorInfoSkillMatrixViewModel operatorInfoSkillMatrixViewModel = new OperatorInfoSkillMatrixViewModel();

                    double rowTotalSot50 = Math.Round((skillMatrix.StandardSotInSecond / skillMatrix.SotScore) * 50, 2);
                    double rowTotalRft50 = Math.Round((skillMatrix.RftScore * 50 / skillMatrix.StandardRft), 2);
                    double rowTotal100 = rowTotalSot50 + rowTotalRft50;
                    if (rowTotal100 < 0) { rowTotal100 = 0; }
                    string grade = GetGrade(rowTotal100);

                    operatorInfoSkillMatrixViewModel.OperationName = skillMatrix.Operation.Name;
                    operatorInfoSkillMatrixViewModel.Score = rowTotal100;
                    operatorInfoSkillMatrixViewModel.Grade = grade;
                    operatorInfoSkillMatrixViewModels.Add(operatorInfoSkillMatrixViewModel);
                }
            }

            operatorInfoViewModel.OperatorInfoSkillMatrixViewModels = operatorInfoSkillMatrixViewModels;

            return new JsonResult(operatorInfoViewModel);
        }

        [HttpPost]
        public JsonResult GetOperationDataById(string data)
        {
            var searchOperationByLineIdViewModel = JsonConvert.DeserializeObject<SearchOperationByLineIdViewModel>(data);
            if (searchOperationByLineIdViewModel.OperationId == null || searchOperationByLineIdViewModel.OperatorId == null)
            {
                return new JsonResult(false);
            }
            var skillMatrices = _context.SkillMatrixs
                .Include(s => s.Operation)
                .Include(s => s.Operator)
                .Where(s => s.OperationId == searchOperationByLineIdViewModel.OperationId)
                .ToList();
            List<OperationSkillMatrixViewModel> operatorInfoSkillMatrixViewModels = new List<OperationSkillMatrixViewModel>();
            List<OperationSkillMatrixViewModel> FoundOperatorInfoSkillMatrixViewModels = new List<OperationSkillMatrixViewModel>();
            if (skillMatrices == null)
            {
                return new JsonResult(false);
            }
            if (skillMatrices.Count != 0)
            {
                foreach (var skillMatrix in skillMatrices)
                {
                    if (searchOperationByLineIdViewModel.OperatorId.Exists(m => m == skillMatrix.OperatorId))
                    {
                        OperationSkillMatrixViewModel operatorInfoSkillMatrixViewModel = new OperationSkillMatrixViewModel();

                        double rowTotalSot50 = Math.Round((skillMatrix.StandardSotInSecond / skillMatrix.SotScore) * 50, 2);
                        double rowTotalRft50 = Math.Round((skillMatrix.RftScore * 50 / skillMatrix.StandardRft), 2);
                        double rowTotal100 = rowTotalSot50 + rowTotalRft50;
                        if (rowTotal100 < 0) { rowTotal100 = 0; }
                        string grade = GetGrade(rowTotal100);

                        operatorInfoSkillMatrixViewModel.OperatorName = skillMatrix.Operator.Name;
                        operatorInfoSkillMatrixViewModel.OperatorIdCardNumber = skillMatrix.Operator.IdCardNumber;
                        operatorInfoSkillMatrixViewModel.Score = rowTotal100;
                        operatorInfoSkillMatrixViewModel.Grade = grade;
                        operatorInfoSkillMatrixViewModels.Add(operatorInfoSkillMatrixViewModel);
                    }

                }
            }

            return new JsonResult(operatorInfoSkillMatrixViewModels);
        }

        [HttpPost]
        public JsonResult GetOperationData(int id)
        {
            var skillMatrices = _context.SkillMatrixs
                .Include(s => s.Operation)
                .Include(s => s.Operator)
                .Where(s => s.OperationId == id)
                .ToList();
            List<OperationSkillMatrixViewModel> operatorInfoSkillMatrixViewModels = new List<OperationSkillMatrixViewModel>();

            if (skillMatrices.Count != 0)
            {
                foreach (var skillMatrix in skillMatrices)
                {
                    OperationSkillMatrixViewModel operatorInfoSkillMatrixViewModel = new OperationSkillMatrixViewModel();

                    double rowTotalSot50 = Math.Round((skillMatrix.StandardSotInSecond / skillMatrix.SotScore) * 50, 2);
                    double rowTotalRft50 = Math.Round((skillMatrix.RftScore * 50 / skillMatrix.StandardRft), 2);
                    double rowTotal100 = rowTotalSot50 + rowTotalRft50;
                    if (rowTotal100 < 0) { rowTotal100 = 0; }
                    string grade = GetGrade(rowTotal100);

                    operatorInfoSkillMatrixViewModel.OperatorName = skillMatrix.Operator.Name;
                    operatorInfoSkillMatrixViewModel.OperatorIdCardNumber = skillMatrix.Operator.IdCardNumber;
                    operatorInfoSkillMatrixViewModel.Score = rowTotal100;
                    operatorInfoSkillMatrixViewModel.Grade = grade;
                    operatorInfoSkillMatrixViewModels.Add(operatorInfoSkillMatrixViewModel);
                }
            }

            return new JsonResult(operatorInfoSkillMatrixViewModels);
        }

        private BottleNeckViewModel GetBottleNeckViewModel(List<LineBalancingStateViewModel> lineBalancingStateViewModels)
        {
            List<double> graphTimes = new List<double>();
            BottleNeckViewModel bottleNeckViewModel = new BottleNeckViewModel();
            if (lineBalancingStateViewModels.Count != 0)
            {
                var firstBottleNeck = lineBalancingStateViewModels.OrderByDescending(r => r.GraphTime).FirstOrDefault();
                var secondBottleNeck = lineBalancingStateViewModels.OrderByDescending(r => r.GraphTime).Skip(1).FirstOrDefault();

                bottleNeckViewModel.FirstBottleNeckValue = firstBottleNeck.GraphTime;
                bottleNeckViewModel.FirstBottleNeckName = firstBottleNeck.OperationName;
                bottleNeckViewModel.SecondBottleNeckValue = secondBottleNeck.GraphTime;
                bottleNeckViewModel.SecondBottleNeckName = secondBottleNeck.OperationName;

                return bottleNeckViewModel;
            }
            else
            {
                return bottleNeckViewModel;
            }
        }

        private string GetFirstBottleNeckOperationName(List<LineBalancingStateViewModel> lineBalancingStateViewModels)
        {
            List<double> graphTimes = new List<double>();
            if (lineBalancingStateViewModels.Count != 0)
            {
                foreach (var lineBalancingStateViewModel in lineBalancingStateViewModels)
                {
                    graphTimes.Add((double)lineBalancingStateViewModel.GraphTime);
                }
                int index = graphTimes.IndexOf(graphTimes.Max());
                return lineBalancingStateViewModels[index].OperationName;
            }
            else
            {
                return "";
            }
        }

        private double GetFirstBottleNeck(List<LineBalancingStateViewModel> lineBalancingStateViewModels)
        {
            List<double> graphTimes = new List<double>();
            foreach (var lineBalancingStateViewModel in lineBalancingStateViewModels)
            {
                graphTimes.Add((double)lineBalancingStateViewModel.GraphTime);
            }
            return graphTimes.Max();
        }

        private double GetDailyOutput(double totalNumberOfOperators, double totalProcessingTime, double organizationalEfficiency)
        {
            //return ((dailyWorkHours * 3600 * totalNumberOfOperators / (totalProcessingTime * (1 + allowance / 100))) * (organizationalEfficiency));
            return ((3600 * totalNumberOfOperators / (totalProcessingTime)) * (organizationalEfficiency));
        }

        private double GetBpt(double totalCycleTime, double totalNumberOfOperators)
        {
            return totalCycleTime / totalNumberOfOperators;
        }

        private double GetTotalCycleTime(List<LineBalancingStateViewModel> lineBalancingStateViewModels)
        {
            double totalCycleTimes = 0;
            if (lineBalancingStateViewModels.Count != 0)
            {
                foreach (var lineBalancingStateViewModel in lineBalancingStateViewModels)
                {
                    totalCycleTimes += (double)lineBalancingStateViewModel.TotalCycleTime;
                }
            }

            return totalCycleTimes;
        }

        private double GetTotalNumberOfOperators(List<LineBalancingStateViewModel> lineBalancingStateViewModels)
        {
            List<int> operators = new List<int>();
            if (lineBalancingStateViewModels.Count != 0)
            {
                foreach (var lineBalancingStateViewModel in lineBalancingStateViewModels)
                {
                    if (lineBalancingStateViewModel.OperatorNo1 != null)
                    {
                        if (!operators.Exists(m => m == lineBalancingStateViewModel.OperatorNo1))
                        {
                            operators.Add((int)lineBalancingStateViewModel.OperatorNo1);
                        }
                    }
                    if (lineBalancingStateViewModel.OperatorNo2 != null)
                    {
                        if (!operators.Exists(m => m == lineBalancingStateViewModel.OperatorNo2))
                        {
                            operators.Add((int)lineBalancingStateViewModel.OperatorNo2);
                        }
                    }
                    if (lineBalancingStateViewModel.OperatorNo3 != null)
                    {
                        if (!operators.Exists(m => m == lineBalancingStateViewModel.OperatorNo3))
                        {
                            operators.Add((int)lineBalancingStateViewModel.OperatorNo3);
                        }
                    }
                    if (lineBalancingStateViewModel.OperatorNo4 != null)
                    {
                        if (!operators.Exists(m => m == lineBalancingStateViewModel.OperatorNo4))
                        {
                            operators.Add((int)lineBalancingStateViewModel.OperatorNo4);
                        }
                    }
                }
            }
            return operators.Count;
        }

        private List<LineBalancingStateViewModel> GetLineBalancingStateViewModels(List<LineBalancingState> lineBalancingStates)
        {
            List<LineBalancingStateViewModel> lineBalancingStateViewModels = new List<LineBalancingStateViewModel>();
            if (lineBalancingStates.Count != 0)
            {
                foreach (var lineBalancingState in lineBalancingStates)
                {
                    var lineBalancingStateViewModel = new LineBalancingStateViewModel();
                    lineBalancingStateViewModel.Id = lineBalancingState.Id;
                    lineBalancingStateViewModel.ParticularLineBalancingId = lineBalancingState.ParticularLineBalancingId;
                    lineBalancingStateViewModel.OperationName = lineBalancingState.OperationName;
                    lineBalancingStateViewModel.OperationId = lineBalancingState.OperationId;
                    lineBalancingStateViewModel.MachineId = lineBalancingState.MachineId;

                    lineBalancingStateViewModel.OperatorId1 = lineBalancingState.OperatorId1;
                    lineBalancingStateViewModel.OperatorId2 = lineBalancingState.OperatorId2;
                    lineBalancingStateViewModel.OperatorId3 = lineBalancingState.OperatorId3;
                    lineBalancingStateViewModel.OperatorId4 = lineBalancingState.OperatorId4;

                    lineBalancingStateViewModel.OperatorNo1 = lineBalancingState.OperatorNo1;
                    lineBalancingStateViewModel.OperatorNo2 = lineBalancingState.OperatorNo2;
                    lineBalancingStateViewModel.OperatorNo3 = lineBalancingState.OperatorNo3;
                    lineBalancingStateViewModel.OperatorNo4 = lineBalancingState.OperatorNo4;

                    //Make the operationname like O1 (1_3)
                    lineBalancingStateViewModel.OperationName += " ( ";

                    if (lineBalancingStateViewModel.OperatorNo1 != null)
                    {
                        lineBalancingStateViewModel.OperationName += lineBalancingStateViewModel.OperatorNo1;
                    }
                    if (lineBalancingStateViewModel.OperatorNo2 != null)
                    {
                        lineBalancingStateViewModel.OperationName += "_" + lineBalancingStateViewModel.OperatorNo2;
                    }
                    if (lineBalancingStateViewModel.OperatorNo3 != null)
                    {
                        lineBalancingStateViewModel.OperationName += "_" + lineBalancingStateViewModel.OperatorNo3;
                    }
                    if (lineBalancingStateViewModel.OperatorNo4 != null)
                    {
                        lineBalancingStateViewModel.OperationName += "_" + lineBalancingStateViewModel.OperatorNo4;
                    }
                    lineBalancingStateViewModel.OperationName += " )";

                    lineBalancingStateViewModel.CycleTime1 = lineBalancingState.CycleTime1;
                    lineBalancingStateViewModel.CycleTime2 = lineBalancingState.CycleTime2;
                    lineBalancingStateViewModel.CycleTime3 = lineBalancingState.CycleTime3;
                    lineBalancingStateViewModel.CycleTime4 = lineBalancingState.CycleTime4;

                    lineBalancingStateViewModel.AllocatedTime1 = lineBalancingState.AllocatedTime1;
                    lineBalancingStateViewModel.AllocatedTime2 = lineBalancingState.AllocatedTime2;
                    lineBalancingStateViewModel.AllocatedTime3 = lineBalancingState.AllocatedTime3;
                    lineBalancingStateViewModel.AllocatedTime4 = lineBalancingState.AllocatedTime4;

                    lineBalancingStateViewModel.Output1 = lineBalancingState.Output1;
                    lineBalancingStateViewModel.Output2 = lineBalancingState.Output2;
                    lineBalancingStateViewModel.Output3 = lineBalancingState.Output3;
                    lineBalancingStateViewModel.Output4 = lineBalancingState.Output4;
                    lineBalancingStateViewModel.TotalOutput = 0;
                    lineBalancingStateViewModel.TotalShare = 0;
                    lineBalancingStateViewModel.TotalCycleTime = 0;
                    lineBalancingStateViewModel.GraphTime = 0;

                    //Total Output
                    if (lineBalancingStateViewModel.Output1 != null)
                    {
                        lineBalancingStateViewModel.TotalOutput += lineBalancingStateViewModel.Output1;
                    }
                    if (lineBalancingStateViewModel.Output2 != null)
                    {
                        lineBalancingStateViewModel.TotalOutput += lineBalancingStateViewModel.Output2;
                    }
                    if (lineBalancingStateViewModel.Output3 != null)
                    {
                        lineBalancingStateViewModel.TotalOutput += lineBalancingStateViewModel.Output3;
                    }
                    if (lineBalancingStateViewModel.Output4 != null)
                    {
                        lineBalancingStateViewModel.TotalOutput += lineBalancingStateViewModel.Output4;
                    }
                    lineBalancingStateViewModel.GraphTime = 3600 / lineBalancingStateViewModel.TotalOutput;
                    //Total Share
                    if (lineBalancingStateViewModel.AllocatedTime1 != null)
                    {
                        lineBalancingStateViewModel.Share1 = lineBalancingStateViewModel.AllocatedTime1 / 60;
                        lineBalancingStateViewModel.TotalShare += lineBalancingStateViewModel.AllocatedTime1 / 60;
                    }
                    if (lineBalancingStateViewModel.AllocatedTime2 != null)
                    {
                        lineBalancingStateViewModel.Share2 = lineBalancingStateViewModel.AllocatedTime2 / 60;
                        lineBalancingStateViewModel.TotalShare += lineBalancingStateViewModel.AllocatedTime2 / 60;
                    }
                    if (lineBalancingStateViewModel.AllocatedTime3 != null)
                    {
                        lineBalancingStateViewModel.Share3 = lineBalancingStateViewModel.AllocatedTime3 / 60;
                        lineBalancingStateViewModel.TotalShare += lineBalancingStateViewModel.AllocatedTime3 / 60;
                    }
                    if (lineBalancingStateViewModel.AllocatedTime4 != null)
                    {
                        lineBalancingStateViewModel.Share3 = lineBalancingStateViewModel.AllocatedTime4 / 60;
                        lineBalancingStateViewModel.TotalShare += lineBalancingStateViewModel.AllocatedTime4 / 60;
                    }

                    lineBalancingStateViewModel.TotalCycleTime = lineBalancingStateViewModel.GraphTime * lineBalancingStateViewModel.TotalShare;

                    //Add allowance
                    var machine = _context.Machines.FirstOrDefault(m => m.Id == lineBalancingStateViewModel.MachineId);
                    if (machine != null)
                    {
                        if (machine.Total != null)
                        {
                            //lineBalancingStateViewModel.TotalCycleTime += lineBalancingStateViewModel.TotalCycleTime * (machine.Total / 100);
                        }
                    }

                    lineBalancingStateViewModels.Add(lineBalancingStateViewModel);
                }
            }

            return lineBalancingStateViewModels;
        }

        private string GetGrade(double number)
        {
            string grade = "";
            if (number >= 91)
            {
                grade = "A";
            }
            else if (number >= 81 && number < 91)
            {
                grade = "B";
            }
            else if (number >= 71 && number < 81)
            {
                grade = "C";
            }
            else if (number >= 61 && number < 71)
            {
                grade = "D";
            }
            else if (number < 61)
            {
                grade = "E";
            }

            return grade;
        }
    }
}