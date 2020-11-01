using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dynamo.Data;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Incentives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DecathlonDynamoErpApp.Controllers.Incentives
{
    [Authorize]
    public class MonthlyVariableValuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MonthlyVariableValuesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("MonthlyVariableValues/Assign/{sectionId}/{month}/{year}")]
        public async Task<IActionResult> Assign(int? sectionId, int? month, int? year)
        {
            if (sectionId == null || month == null || year == null)
            {
                return NotFound();
            }
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
            var monthlyVariableValues = _context.MonthlyVariableValues
                .Include(m => m.IncentiveVariable)
                .Include(m => m.Section)
                .Where(m => m.Month == month && m.Year == year && m.SectionId == sectionId)
                .ToList();

            var monthlySectionEarningPoint = _context.MonthlySectionEarningPoints
               .Include(m => m.Section)
               .FirstOrDefault(m => m.Month == month && m.Year == year && m.SectionId == sectionId);

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            ViewData["Date"] = date;
            ViewData["Section"] = _context.Sections.FirstOrDefault(m => m.Id == sectionId);
            ViewData["Sections"] = _context.Sections.Where(m => m.FactoryId == loggedInUser.FactoryId).ToList();
            ViewData["MonthlyVariableValues"] = monthlyVariableValues;
            ViewData["MonthlySectionEarningPoint"] = monthlySectionEarningPoint;
            ViewData["IncentiveVariables"] = _context.IncentiveVariables.ToList();

            return View(monthlyVariableValues);
        }

        [HttpPost]
        public JsonResult Save(string data)
        {
            var monthlyVariableValue = JsonConvert.DeserializeObject<MonthlyVariableValue>(data);


            if (monthlyVariableValue.Id != -1)
            {
                _context.MonthlyVariableValues.Update(monthlyVariableValue);
                _context.SaveChanges();
                return new JsonResult(monthlyVariableValue.Id);
            }
            else
            {
                monthlyVariableValue.Id = 0;
                _context.MonthlyVariableValues.Add(monthlyVariableValue);
                _context.SaveChanges();
                return new JsonResult(monthlyVariableValue.Id);
            }
        }

        [HttpPost]
        public JsonResult SaveMonthlyEarningPoint(string data)
        {
            var monthlySectionEarningPoints = JsonConvert.DeserializeObject<MonthlySectionEarningPoint>(data);


            if (monthlySectionEarningPoints.Id != 0)
            {
                _context.MonthlySectionEarningPoints.Update(monthlySectionEarningPoints);
                _context.SaveChanges();
                return new JsonResult(monthlySectionEarningPoints.Id);
            }
            else
            {
                monthlySectionEarningPoints.Id = 0;
                _context.MonthlySectionEarningPoints.Add(monthlySectionEarningPoints);
                _context.SaveChanges();
                return new JsonResult(monthlySectionEarningPoints.Id);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var monthlyVariableValue = await _context.MonthlyVariableValues.FindAsync(id);
                _context.MonthlyVariableValues.Remove(monthlyVariableValue);
                await _context.SaveChangesAsync();
                return new JsonResult(true);
            }
            catch
            {
                return new JsonResult(false);
            }
        }

        [HttpPost]
        public JsonResult SaveAll(string monthlyVariableValuesJson)
        {
            var error = false;
            var count = 0;
            var monthlyVariableValues = JsonConvert.DeserializeObject<List<MonthlyVariableValue>>(monthlyVariableValuesJson);
            foreach (var monthlyVariableValue in monthlyVariableValues)
            {
                if (monthlyVariableValue.Id != -1)
                {
                    _context.MonthlyVariableValues.Update(monthlyVariableValue);
                    _context.SaveChanges();
                    try
                    {
                        _context.SaveChanges();
                        count++;
                    }
                    catch
                    {
                        error = true;
                    }
                }
                else
                {
                    monthlyVariableValue.Id = 0;
                    _context.MonthlyVariableValues.Add(monthlyVariableValue);
                    try
                    {
                        _context.SaveChanges();
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

    }
}