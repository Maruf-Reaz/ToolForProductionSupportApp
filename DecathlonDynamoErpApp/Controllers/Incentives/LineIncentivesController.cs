using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dynamo.Data;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Incentives;
using Dynamo.Model.Incentives.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DecathlonDynamoErpApp.Controllers.Incentives
{
    [Authorize]
    public class LineIncentivesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LineIncentivesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var lineIncenticves = _context.LineIncentives.Include(m => m.Line).ThenInclude(d => d.Section);
        //    return View(await lineIncenticves.ToListAsync());
        //    //return View();
        //}

        [HttpGet("LineIncentives/MonthlyIncentive/{lineId}/{month}/{year}")]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<IActionResult> MonthlyIncentive(int? lineId, int? month, int? year)
        {
            var line = await _context.Lines.FirstOrDefaultAsync(m => m.Id == lineId);
            var dateString = "";
            DateTime date;
            if (month < 10)
            {
                dateString = "01-0" + month + "-" + year;
            }
            else
            {
                dateString = "01-" + month + "-" + year;
            }
            try
            {
                //date = DateTime.Parse(dateString);
                date = DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                return NotFound();
            }
            var daysInMonth = DateTime.DaysInMonth((int)year, (int)month);
            List<LineIncentiveViewModel> lineIncentiveViewModels = new List<LineIncentiveViewModel>();
            for (int i = 0; i < daysInMonth; i++)
            {
                lineIncentiveViewModels.Add(new LineIncentiveViewModel { DateString = date.AddDays(i).ToString("dd-MM-yyyy"), LineIncentive = null });
            }

            var lineIncentives = await _context.LineIncentives
                .Where(m => m.LineId == lineId && m.LineIncentiveDateTime.Month == month && m.LineIncentiveDateTime.Year == year)
                .ToListAsync();

            double total = 0;
            double numberOfIncentive = 0;
            double average = 0;
            if (lineIncentives.Count != 0)
            {
                foreach (var lineIncentive in lineIncentives)
                {
                    var day = lineIncentive.LineIncentiveDateTime.Day - 1;
                    lineIncentiveViewModels[day].LineIncentive = lineIncentive;
                    List<PointValue> pointValues = _context.PointValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
                    if (pointValues.Count != 0)
                    {
                        lineIncentiveViewModels[day].PointValues = pointValues.ConvertAll(x => new LineIncentiveVariableValueViewModel
                        {
                            IncentiveVariableId = x.IncentiveVariableId,
                            Value = x.Value
                        });
                    }
                    List<AchievedValue> achievedValues = _context.AchievedValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
                    if (achievedValues.Count != 0)
                    {
                        lineIncentiveViewModels[day].AchievedValues = achievedValues.ConvertAll(x => new LineIncentiveVariableValueViewModel
                        {
                            IncentiveVariableId = x.IncentiveVariableId,
                            Value = x.Value
                        });
                    }
                    List<TargetValue> targetValues = _context.TargetValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
                    if (targetValues.Count != 0)
                    {
                        lineIncentiveViewModels[day].TargetValues = targetValues.ConvertAll(x => new LineIncentiveVariableValueViewModel
                        {
                            IncentiveVariableId = x.IncentiveVariableId,
                            Value = x.Value
                        });
                    }
                    if (lineIncentive.Enable)
                    {
                        total += (double)lineIncentive.Total;
                        numberOfIncentive++;
                    }

                }
            }

            if (numberOfIncentive != 0)
            {
                average = Math.Round((total / numberOfIncentive), 2);
            }

            var monthlySectionEarningPoint = _context.MonthlySectionEarningPoints
               .Include(m => m.Section)
               .FirstOrDefault(m => m.Month == month && m.Year == year && m.SectionId == line.SectionId);

            var monthlyVariableValues = _context.MonthlyVariableValues
               .Include(m => m.IncentiveVariable)
               .Include(m => m.Section)
               .Where(m => m.Month == month && m.Year == year && m.SectionId == line.SectionId)
               .ToList();

            ViewData["Line"] = line;
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["Lines"] = await _context.Lines.Where(m => m.Section.FactoryId == loggedInUser.FactoryId).ToListAsync();
            ViewData["LineEarningPoint"] = await _context.MonthlySectionEarningPoints
                .FirstOrDefaultAsync(m => m.SectionId == line.SectionId
                && m.Month == month
                && m.Year == year);

            //ViewData["AssignLineVariable"] = await _context.AssignLineVariables.FirstOrDefaultAsync(m => m.LineId == lineId);
            ViewData["Date"] = date;
            ViewData["MonthlySectionEarningPoint"] = monthlySectionEarningPoint;
            ViewData["MonthlyVariableValues"] = monthlyVariableValues;
            ViewData["AverageTotal"] = average;
            ViewData["MonthName"] = date.ToString("MMMM", CultureInfo.InvariantCulture);
            return View(lineIncentiveViewModels);
        }

        [HttpPost]
        public JsonResult Save(string data)
        {
            var lineIncentiveSaveViewModel = JsonConvert.DeserializeObject<LineIncentiveSaveViewModel>(data);

            var lineIncentive = new LineIncentive();
            lineIncentive.LineId = lineIncentiveSaveViewModel.LineId;
            lineIncentive.Enable = lineIncentiveSaveViewModel.Enable;
            lineIncentive.Comments = lineIncentiveSaveViewModel.Comments;
            lineIncentive.Total = lineIncentiveSaveViewModel.Total;
            lineIncentive.LineIncentiveDateTime = DateTime.ParseExact(lineIncentiveSaveViewModel.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            if (lineIncentiveSaveViewModel.rowId != -1)
            {
                lineIncentive.Id = lineIncentiveSaveViewModel.rowId;
                _context.LineIncentives.Update(lineIncentive);
                //await _context.SaveChangesAsync();
                _context.SaveChanges();
            }
            else
            {
                _context.LineIncentives.Add(lineIncentive);
                //await _context.SaveChangesAsync();
                _context.SaveChanges();
            }

            List<TargetValue> targetValues = _context.TargetValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
            if (targetValues.Count != 0)
            {
                _context.RemoveRange(targetValues);
            }

            List<AchievedValue> achievedValues = _context.AchievedValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
            if (achievedValues.Count != 0)
            {
                _context.RemoveRange(achievedValues);
            }

            List<PointValue> pointValues = _context.PointValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
            if (pointValues.Count != 0)
            {
                _context.RemoveRange(pointValues);
            }

            List<PointValue> pointsValuesVM = lineIncentiveSaveViewModel.PointValues.ConvertAll(x => new PointValue
            {
                LineIncentiveId = lineIncentive.Id,
                IncentiveVariableId = x.IncentiveVariableId,
                Value = x.Value
            });

            List<AchievedValue> achievedValuesVM = lineIncentiveSaveViewModel.AchievedValues.ConvertAll(x => new AchievedValue
            {
                LineIncentiveId = lineIncentive.Id,
                IncentiveVariableId = x.IncentiveVariableId,
                Value = x.Value
            });

            List<TargetValue> targetValuesVM = lineIncentiveSaveViewModel.TargetValues.ConvertAll(x => new TargetValue
            {
                LineIncentiveId = lineIncentive.Id,
                IncentiveVariableId = x.IncentiveVariableId,
                Value = x.Value
            });

            if (pointsValuesVM.Count != 0)
            {
                _context.AddRange(pointsValuesVM);
                _context.SaveChanges();
            }
            if (achievedValuesVM.Count != 0)
            {
                _context.AddRange(achievedValuesVM);
                _context.SaveChanges();
            }
            if (targetValuesVM.Count != 0)
            {
                _context.AddRange(targetValuesVM);
                _context.SaveChanges();
            }

            return new JsonResult(lineIncentive.Id);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var lineIncentive = await _context.LineIncentives.FindAsync(id);
                //Delete all other related values
                var targetValues = _context.TargetValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
                if (targetValues.Count != 0)
                {
                    _context.RemoveRange(targetValues);
                    _context.SaveChanges();
                }

                var achievedValues = _context.AchievedValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
                if (achievedValues.Count != 0)
                {
                    _context.RemoveRange(achievedValues);
                    _context.SaveChanges();
                }

                var pointValues = _context.PointValues.Where(m => m.LineIncentiveId == lineIncentive.Id).ToList();
                if (pointValues.Count != 0)
                {
                    _context.RemoveRange(pointValues);
                    _context.SaveChanges();
                }

                _context.LineIncentives.Remove(lineIncentive);
                await _context.SaveChangesAsync();
                return new JsonResult(true);
            }
            catch
            {
                return new JsonResult(false);
            }
        }
    }
}