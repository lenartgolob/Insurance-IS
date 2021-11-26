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
            return View();
        }

        // POST: InsuranceSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceSubjectID,Title,Description,EstimatedValue")] InsuranceSubject insuranceSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceSubject);
        }

        // GET: InsuranceSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubject = await _context.InsuranceSubject.FindAsync(id);
            if (insuranceSubject == null)
            {
                return NotFound();
            }
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceSubjectExists(insuranceSubject.InsuranceSubjectID))
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
            return View(insuranceSubject);
        }

        // GET: InsuranceSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubject = await _context.InsuranceSubject
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
