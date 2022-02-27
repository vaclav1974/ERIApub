using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERIA.Models;

namespace ERIA.Controllers
{
    public class WorkTasksController : Controller
    {
        private readonly ERIAContext _context;

        public WorkTasksController(ERIAContext context)
        {
            _context = context;
        }

        // GET: WorkTasks
         public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["TypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Type" : "";
            ViewData["CurrentFilter"] = searchString;

            var tasks = from s in _context.WorkTask
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(s => s.Input.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tasks = tasks.OrderByDescending(s => s.Input);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(s => s.FromDate);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(s => s.FromDate);
                    break;
                case "Type":
                    tasks = tasks.OrderByDescending(s => s.WorkType);
                    break;
                default:
                    tasks = tasks.OrderBy(s => s.FromDate);
                    break;
            }
            return View(await tasks.Include(s=>s.WorkType).AsNoTracking().ToListAsync());
        }

        // GET: WorkTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTask
                .Include(w => w.WorkType)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }

        // GET: WorkTasks/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.WorkType, "TypeId", "Description");
            return View();
        }

        // POST: WorkTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,Input,TillDate,FromDate,TypeId")] WorkTask workTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.WorkType, "TypeId", "Description", workTask.TypeId);
            return View(workTask);
        }

        // GET: WorkTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTask.FindAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.WorkType, "TypeId", "Description", workTask.TypeId);
            return View(workTask);
        }

        // POST: WorkTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,Input,TillDate,FromDate,TypeId")] WorkTask workTask)
        {
            if (id != workTask.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTaskExists(workTask.TaskId))
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
            ViewData["TypeId"] = new SelectList(_context.WorkType, "TypeId", "Description", workTask.TypeId);
            return View(workTask);
        }

        // GET: WorkTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTask
                .Include(w => w.WorkType)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }

        // POST: WorkTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workTask = await _context.WorkTask.FindAsync(id);
            _context.WorkTask.Remove(workTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTaskExists(int id)
        {
            return _context.WorkTask.Any(e => e.TaskId == id);
        }
    }
}
