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
    public class InsuranceSubjectsController : Controller
    {
        private readonly InsuranceContext _context;

        public InsuranceSubjectsController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: InsuranceSubjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.InsuranceSubject.ToListAsync());
        }

        // GET: InsuranceSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubject = await _context.InsuranceSubject
                .Include(t => t.InsuranceSubjectType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubjectID == id);
            if (insuranceSubject == null)
            {
                return NotFound();
            }

            return View(insuranceSubject);
        }

        // GET: InsuranceSubjects/Create
        public IActionResult Create()
        {
            PopulateSubjectTypesDropDownList();
            return View();
        }

        // POST: InsuranceSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceSubjectID,Title,Description,EstimatedValue,InsuranceSubjectTypeID")] InsuranceSubject insuranceSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateSubjectTypesDropDownList(insuranceSubject.InsuranceSubjectTypeID);
            return View(insuranceSubject);
        }

        // GET: InsuranceSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubject = await _context.InsuranceSubject
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubjectID == id);
            if (insuranceSubject == null)
            {
                return NotFound();
            }
            PopulateSubjectTypesDropDownList(insuranceSubject.InsuranceSubjectTypeID);
            return View(insuranceSubject);
        }

        // POST: InsuranceSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceSubjectID,Title,Description,EstimatedValue")] InsuranceSubject insuranceSubject)
        {
            if (id != insuranceSubject.InsuranceSubjectID)
            {
                return NotFound();
            }

                var insuranceSubjectToUpdate = await _context.InsuranceSubject
                    .FirstOrDefaultAsync(s => s.InsuranceSubjectID == id);

                if (await TryUpdateModelAsync<InsuranceSubject>(insuranceSubjectToUpdate,
                    "",
                    s => s.Title, s => s.Description, s => s.EstimatedValue, s => s.InsuranceSubjectTypeID))
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
                PopulateSubjectTypesDropDownList(insuranceSubjectToUpdate.InsuranceSubjectTypeID);
                return View(insuranceSubjectToUpdate);

            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         _context.Update(insuranceSubject);
            //         await _context.SaveChangesAsync();
            //     }
            //     catch (DbUpdateConcurrencyException)
            //     {
            //         if (!InsuranceSubjectExists(insuranceSubject.InsuranceSubjectID))
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
            // return View(insuranceSubject);
        }

        private void PopulateSubjectTypesDropDownList(object selectedInsuranceSubjectType = null)
        {
            var insuranceSubjectTypesQuery = from t in _context.InsuranceSubjectType
                                orderby t.Title
                                select t;
            ViewBag.InsuranceSubjectTypeID = new SelectList(insuranceSubjectTypesQuery.AsNoTracking(), "InsuranceSubjectTypeID", "Title", selectedInsuranceSubjectType);
        }

        // GET: InsuranceSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubject = await _context.InsuranceSubject
                .Include(t => t.InsuranceSubjectType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubjectID == id);
            if (insuranceSubject == null)
            {
                return NotFound();
            }

            return View(insuranceSubject);
        }

        // POST: InsuranceSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceSubject = await _context.InsuranceSubject.FindAsync(id);
            _context.InsuranceSubject.Remove(insuranceSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceSubjectExists(int id)
        {
            return _context.InsuranceSubject.Any(e => e.InsuranceSubjectID == id);
        }
    }
}
