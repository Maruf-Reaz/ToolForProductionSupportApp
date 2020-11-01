using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.Sot;
using Microsoft.AspNetCore.Hosting;
using Dynamo.Model.Sot.ViewModels;
using Dynamo.Model.Common.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;
using jQueryDatatableServerSideNetCore22.Models.AuxiliaryModels;
using System.Collections.Generic;
using jQueryDatatableServerSideNetCore22.Extensions;

namespace DecathlonDynamoErpApp.Controllers.Sot
{
    [Authorize]
    public class SotModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SotModelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: SotModels
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
            return View(await _context.SotModels.Where(m => m.FactoryId == loggedInUser.FactoryId).Include(s => s.Process).Include(s => s.SignSport).ToListAsync());
        }

        // GET: SotModels/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sotModel = await _context.SotModels
        //        .Include(s => s.Process)
        //        .Include(s => s.SignSport)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (sotModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sotModel);
        //}

        // GET: SotModels/Create
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Create()
        {
            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["ProcessId"] = new SelectList(_context.Processes.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name");
            ViewData["SignSportId"] = new SelectList(_context.SignSports.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name");

            return View();
        }

        // POST: SotModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Create(SotModelViewModel sotModelViewModel)
        {
            if (ModelState.IsValid)
            {
                string photo = "No File";
                if (sotModelViewModel.Photo != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = sotModelViewModel.Photo.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    photo = uniqueFileName;
                    InputFile fileUpload = new InputFile(_hostingEnvironment);
                    fileUpload.Uploadfile("files/sot_models", sotModelViewModel.Photo, photo);
                }

                var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var sotModel = new SotModel
                {
                    FactoryId = loggedInUser.FactoryId,
                    Name = sotModelViewModel.Name,
                    Dipl = sotModelViewModel.Dipl,
                    ProcessId = sotModelViewModel.ProcessId,
                    SignSportId = sotModelViewModel.SignSportId,
                    Photo = photo
                };

                _context.Add(sotModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sotModelViewModel);
        }

        // GET: SotModels/Edit/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sotModel = await _context.SotModels.FirstOrDefaultAsync(m => m.Id == id);
            if (sotModel == null)
            {
                return NotFound();
            }

            var sotModelViewModel = new SotModelViewModel
            {
                Id = sotModel.Id,
                FactoryId = sotModel.FactoryId,
                Name = sotModel.Name,
                Dipl = sotModel.Dipl,
                ProcessId = sotModel.ProcessId,
                SignSportId = sotModel.SignSportId,
                OldPhoto = sotModel.Photo
            };

            var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            ViewData["ProcessId"] = new SelectList(_context.Processes.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", sotModelViewModel.ProcessId);
            ViewData["SignSportId"] = new SelectList(_context.SignSports.Where(m => m.FactoryId == loggedInUser.FactoryId), "Id", "Name", sotModelViewModel.SignSportId);

            return View(sotModelViewModel);
        }

        // POST: SotModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfArchive,YSSUserOfArchive,MAFUserOfArchive,RFLUserOfArchive,APEXUserOfArchive,EDISONUserOfArchive")]
        public async Task<IActionResult> Edit(int id, SotModelViewModel sotModelViewModel)
        {
            if (id != sotModelViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var sotModel = new SotModel
                {
                    Id = sotModelViewModel.Id,
                    FactoryId = sotModelViewModel.FactoryId,
                    Name = sotModelViewModel.Name,
                    Dipl = sotModelViewModel.Dipl,
                    ProcessId = sotModelViewModel.ProcessId,
                    SignSportId = sotModelViewModel.SignSportId,
                    Photo = sotModelViewModel.OldPhoto
                };

                if (sotModelViewModel.Photo != null)
                {
                    string uniqueFileName = null;
                    string stringCutted = sotModelViewModel.Photo.FileName.Split('.').Last();
                    uniqueFileName = Guid.NewGuid().ToString() + "." + stringCutted;
                    sotModel.Photo = uniqueFileName;
                    InputFile fileUpload = new InputFile(_hostingEnvironment);
                    if (sotModelViewModel.OldPhoto.ToLower() == "no file")
                    {
                        fileUpload.Uploadfile("files/sot_models", sotModelViewModel.Photo, uniqueFileName);
                    }
                    else
                    {
                        fileUpload.Updatefile("files/sot_models", sotModelViewModel.Photo, sotModelViewModel.OldPhoto, uniqueFileName);
                    }
                }

                try
                {
                    _context.Update(sotModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SotModelExists(sotModel.Id))
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
            return View(sotModelViewModel);
        }

        // GET: SotModels/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sotModel = await _context.SotModels
                .Include(s => s.Process)
                .Include(s => s.SignSport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sotModel == null)
            {
                return NotFound();
            }

            return View(sotModel);
        }

        // POST: SotModels/Delete/5
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sotModel = await _context.SotModels.FindAsync(id);

            InputFile fileUpload = new InputFile(_hostingEnvironment);
            if (sotModel.Photo.ToLower() != "no file")
            {
                fileUpload.DeleteFile("files/sot_models", sotModel.Photo);
            }

            _context.SotModels.Remove(sotModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SotModelExists(int id)
        {
            return _context.SotModels.Any(e => e.Id == id);
        }

        // POST: SotModels/GetDetails/5
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,KSIAdmin,YSSAdmin,MAFAdmin,RFLAdmin,APEXAdmin,EDISONAdmin," +
            "KSIUserOfLineBalancing,KSIUserOfIncentive,KSIUserOfSkillMatrix,KSIUserOfSOT,KSIUserOfArchive," +
            "YSSUserOfLineBalancing,YSSUserOfIncentive,YSSUserOfSkillMatrix,YSSUserOfSOT,YSSUserOfArchive," +
            "MAFUserOfLineBalancing,MAFUserOfIncentive,MAFUserOfSkillMatrix,MAFUserOfSOT,MAFUserOfArchive," +
            "RFLUserOfLineBalancing,RFLUserOfIncentive,RFLUserOfSkillMatrix,RFLUserOfSOT,RFLUserOfArchive," +
            "APEXUserOfLineBalancing,APEXUserOfIncentive,APEXUserOfSkillMatrix,APEXUserOfSOT,APEXUserOfArchive," +
            "EDISONUserOfLineBalancing,EDISONUserOfIncentive,EDISONUserOfSkillMatrix,EDISONUserOfSOT,EDISONUserOfArchive," +
            "KSIViewer,YSSViewer,MAFViewer,RFLViewer,APEXViewer,EDISONViewer")]
        public async Task<JsonResult> GetDetails(int id)
        {
            var sotModel = await _context.SotModels
                .Include(s => s.Process)
                .Include(s => s.SignSport)
                .FirstOrDefaultAsync(m => m.Id == id);

            var sotModelJson = new SotModel { Dipl = sotModel.Dipl, ProcessName = sotModel.Process.Name, SignSportName = sotModel.SignSport.Name, Photo = sotModel.Photo };
            return Json(sotModelJson);
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
            var result = await _context.SotModels.Where(m => m.FactoryId == loggedInUser.FactoryId).Include(s => s.Process).Include(s => s.SignSport).ToListAsync();


            //here
            List<SotModel> sotModels = new List<SotModel>();
            foreach (var item in result)
            {
                SotModel tempSotModel = new SotModel();
                tempSotModel.Id = item.Id;
                tempSotModel.Name = item.Name;
                tempSotModel.Dipl = item.Dipl;
                tempSotModel.ProcessName = item.Process.Name;
                tempSotModel.SignSportName = item.SignSport.Name;
                tempSotModel.Photo = item.Photo;
                sotModels.Add(tempSotModel);
            }

            //here
            if (!string.IsNullOrEmpty(searchBy))
            {
                sotModels = sotModels.Where(r =>

                r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                r.Dipl != null && r.Dipl.ToUpper().Contains(searchBy.ToUpper()) ||
                r.ProcessName != null && r.ProcessName.ToUpper().Contains(searchBy.ToUpper()) ||
                r.SignSportName != null && r.SignSportName.ToUpper().Contains(searchBy.ToUpper())

                ).ToList();
            }

            //here
            sotModels = orderAscendingDirection ? sotModels.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Asc).ToList() : sotModels.AsQueryable().OrderByDynamic(orderCriteria,
                LinqExtensions.Order.Desc).ToList();

            //here
            var filteredResultsCount = sotModels.Count();
            //here
            var totalResultsCount = await _context.SotModels.Where(m => m.FactoryId == loggedInUser.FactoryId).Include(s => s.Process).Include(s => s.SignSport).CountAsync();
            if (dtParameters.Length == -1)
            {
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    //here
                    data = sotModels
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
                    data = sotModels
                  .Skip(dtParameters.Start)
                  .Take(dtParameters.Length)
                  .ToList()
                });
            }
        }
    }
}
