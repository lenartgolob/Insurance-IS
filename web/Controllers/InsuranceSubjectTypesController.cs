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
            return View(await _context.InsuranceSubjectType.ToListAsync());
        }

        // GET: InsuranceSubjectTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubjectType = await _context.InsuranceSubjectType
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
            return View();
        }

        // POST: InsuranceSubjectTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceSubjectTypeID,Title")] InsuranceSubjectType insuranceSubjectType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceSubjectType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceSubjectType);
        }

        // GET: InsuranceSubjectTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubjectType = await _context.InsuranceSubjectType.FindAsync(id);
            if (insuranceSubjectType == null)
            {
                return NotFound();
            }
            return View(insuranceSubjectType);
        }

        // POST: InsuranceSubjectTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceSubjectTypeID,Title")] InsuranceSubjectType insuranceSubjectType)
        {
            if (id != insuranceSubjectType.InsuranceSubjectTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceSubjectType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceSubjectTypeExists(insuranceSubjectType.InsuranceSubjectTypeID))
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
            return View(insuranceSubjectType);
        }

        // GET: InsuranceSubjectTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubjectType = await _context.InsuranceSubjectType
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
