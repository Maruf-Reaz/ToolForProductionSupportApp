using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dynamo.Data;
using Dynamo.Model.Common.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace DecathlonDynamoErpApp.Controllers
{
    [Authorize]
    public class NotificationGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NotificationGroups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NotificationGroups.Include(n => n.Group);
            return View(await applicationDbContext.ToListAsync());

        }

        // GET: NotificationGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationGroup = await _context.NotificationGroups
                .Include(n => n.Group).FirstOrDefaultAsync(m => m.Id == id);
            if (notificationGroup == null)
            {
                return NotFound();
            }

            return View(notificationGroup);
        }

        // GET: NotificationGroups/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: NotificationGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,GroupId")] NotificationGroup notificationGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificationGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "Id", "Id", notificationGroup.GroupId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", notificationGroup.UserId);
            return View(notificationGroup);
        }

        // GET: NotificationGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationGroup = await _context.NotificationGroups.FindAsync(id);
            if (notificationGroup == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "Id", "Id", notificationGroup.GroupId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", notificationGroup.UserId);
            return View(notificationGroup);
        }

        // POST: NotificationGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,GroupId")] NotificationGroup notificationGroup)
        {
            if (id != notificationGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificationGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationGroupExists(notificationGroup.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "Id", "Id", notificationGroup.GroupId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", notificationGroup.UserId);
            return View(notificationGroup);
        }

        // GET: NotificationGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationGroup = await _context.NotificationGroups
                .Include(n => n.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificationGroup == null)
            {
                return NotFound();
            }

            return View(notificationGroup);
        }

        // POST: NotificationGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificationGroup = await _context.NotificationGroups.FindAsync(id);
            _context.NotificationGroups.Remove(notificationGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationGroupExists(int id)
        {
            return _context.NotificationGroups.Any(e => e.Id == id);
        }
    }
}
