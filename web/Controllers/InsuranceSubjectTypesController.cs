using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class InsuranceSubjectTypesController : Controller
    {
        private readonly InsuranceContext _context;

        public InsuranceSubjectTypesController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: InsuranceSubjectTypes
        public async Task<IActionResult> Index()
        {
            var insuranceSubjectTypes = _context.InsuranceSubjectType
                .Include(i => i.InsuranceType)
                .AsNoTracking();
            return View(await insuranceSubjectTypes.ToListAsync());
        }

        // GET: InsuranceSubjectTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubjectType = await _context.InsuranceSubjectType
                .Include(i => i.InsuranceType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubjectTypeID == id);
            if (insuranceSubjectType == null)
            {
                return NotFound();
            }

            return View(insuranceSubjectType);
        }

        // GET: InsuranceSubjectTypes/Create
        public IActionResult Create()
        {
            PopulateTypesDropDownList();
            return View();
        }

        // POST: InsuranceSubjectTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceSubjectTypeID,Title,InsuranceTypeID")] InsuranceSubjectType insuranceSubjectType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceSubjectType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateTypesDropDownList(insuranceSubjectType.InsuranceTypeID);
            return View(insuranceSubjectType);
        }

        // GET: InsuranceSubjectTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubjectType = await _context.InsuranceSubjectType
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubjectTypeID == id);
            if (insuranceSubjectType == null)
            {
                return NotFound();
            }
            PopulateTypesDropDownList(insuranceSubjectType.InsuranceTypeID);
            return View(insuranceSubjectType);
        }

        // POST: InsuranceSubjectTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceSubjectTypeID,Title,InsuranceTypeID")] InsuranceSubjectType insuranceSubjectType)
        {
            if (id != insuranceSubjectType.InsuranceSubjectTypeID)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         _context.Update(insuranceSubjectType);
            //         await _context.SaveChangesAsync();
            //     }
            //     catch (DbUpdateConcurrencyException)
            //     {
            //         if (!InsuranceSubjectTypeExists(insuranceSubjectType.InsuranceSubjectTypeID))
            //         {
            //             return NotFound();
            //         }
            //         else
            //         {
            //             throw;
            //         }
            //     }
            //     return RedirectToAction(nameof(Index));
            // }
            var insuranceSubjectTypeToUpdate = await _context.InsuranceSubjectType
                .FirstOrDefaultAsync(c => c.InsuranceSubjectTypeID == id);

            if (await TryUpdateModelAsync<InsuranceSubjectType>(insuranceSubjectTypeToUpdate,
                "",
                c => c.Title, c => c.InsuranceTypeID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateTypesDropDownList(insuranceSubjectType.InsuranceTypeID);
            return View(insuranceSubjectType);
        }

        private void PopulateTypesDropDownList(object selectedType = null)
        {
            var typesQuery = from t in _context.InsuranceType
                                orderby t.Title
                                select t;
            ViewBag.InsuranceTypeID = new SelectList(typesQuery.AsNoTracking(), "InsuranceTypeID", "Title", selectedType);
        }

        // GET: InsuranceSubjectTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubjectType = await _context.InsuranceSubjectType
                .Include(i => i.InsuranceType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubjectTypeID == id);
            if (insuranceSubjectType == null)
            {
                return NotFound();
            }

            return View(insuranceSubjectType);
        }

        // POST: InsuranceSubjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceSubjectType = await _context.InsuranceSubjectType.FindAsync(id);
            _context.InsuranceSubjectType.Remove(insuranceSubjectType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceSubjectTypeExists(int id)
        {
            return _context.InsuranceSubjectType.Any(e => e.InsuranceSubjectTypeID == id);
        }
    }
}
