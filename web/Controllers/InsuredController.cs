using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;

namespace web.Controllers
{
    [Authorize(Roles = "Administrator, Agent")]
    public class InsuredController : Controller
    {
        private readonly InsuranceContext _context;

        public InsuredController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: Insured
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["FirstNameSortParm"] = sortOrder == "FirstName" ? "first_name_desc" : "FirstName";
            // ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var insured = from i in _context.Insured
                        select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                insured = insured.Where(i => i.LastName.Contains(searchString)
                                    || i.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    insured = insured.OrderByDescending(i => i.LastName);
                    break;
                case "first_name_desc":
                    insured = insured.OrderByDescending(i => i.FirstMidName);
                    break;
                case "FirstName":
                    insured = insured.OrderBy(i => i.FirstMidName);
                    break;
                // case "Date":
                //     insured = insured.OrderBy(i => i.EnrollmentDate);
                //     break;
                // case "date_desc":
                //     insured = insured.OrderByDescending(i => i.EnrollmentDate);
                //     break;
                default:
                    insured = insured.OrderBy(i => i.LastName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Insured>.CreateAsync(insured.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Insured/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insured = await _context.Insured
                .Include(s => s.InsurancePolicies)
                    .ThenInclude(e => e.InsuranceSubtype)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insured == null)
            {
                return NotFound();
            }

            return View(insured);
        }

        // GET: Insured/Create
        public IActionResult Create(string redirect)
        {
            ViewData["redirect"] = redirect;
            return View();
        }

        // POST: Insured/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstMidName,Address,ZipCode,City")] Insured insured, IFormCollection form)
        {
            try {
                if (ModelState.IsValid)
                {
                    insured.FullName = insured.FirstMidName + " " + insured.LastName;
                    _context.Add(insured);
                    await _context.SaveChangesAsync();
                    if(String.Equals(form["redirect"], "insure")){
                        TempData["UserMessage"] = "alert-success";
                        TempData["Title"] = "Success!";
                        TempData["Message"] = "Successfully registered client.";
                        return RedirectToAction("Insure", "Home");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            }
            return View(insured);
        }

        // GET: Insured/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await _context.Insured.FindAsync(id);
            if (insured == null)
            {
                return NotFound();
            }
            return View(insured);
        }

        // POST: Insured/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LastName,FirstMidName,Address,ZipCode,City")] Insured insured)
        {
            if (id != insured.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    insured.FullName = insured.FirstMidName + " " + insured.LastName;
                    _context.Update(insured);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuredExists(insured.ID))
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
            return View(insured);
        }

        // GET: Insured/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await _context.Insured
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insured == null)
            {
                return NotFound();
            }

            return View(insured);
        }

        // POST: Insured/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insured = await _context.Insured.FindAsync(id);
            _context.Insured.Remove(insured);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuredExists(int id)
        {
            return _context.Insured.Any(e => e.ID == id);
        }
    }
}
