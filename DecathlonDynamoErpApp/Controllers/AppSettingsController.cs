using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamo.Data;
using Dynamo.Model.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DecathlonDynamoErpApp.Controllers
{
    [Authorize]
    public class AppSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppSettings
        public ActionResult Index()
        {
            var cssClass = _context.CssClasses.ToList();
            ViewData["CssClass"] = cssClass;
            return View();
        }

        // GET: AppSettings/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppSettings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppSettings/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppSettings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppSettings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppSettings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: AppSettings/ChangeStatus
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var cssClass = _context.CssClasses.Find(id);

            if (cssClass == null)
            {
                return Json(false);
            }

            if (cssClass.IsActive)
            {
                cssClass.IsActive = false;
            }
            else
            {
                cssClass.IsActive = true;
            }

            _context.Update(cssClass);
            _context.SaveChanges();

            return Json(true);
        }
    }
}