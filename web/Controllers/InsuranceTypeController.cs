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
    public class InsuranceTypeController : Controller
    {
        private readonly InsuranceContext _context;

        public InsuranceTypeController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: InsuranceType
        public async Task<IActionResult> Index()
        {
            return View(await _context.InsuranceType.ToListAsync());
        }

        // GET: InsuranceType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceType
                .FirstOrDefaultAsync(m => m.InsuranceTypeID == id);
            if (insuranceType == null)
            {
                return NotFound();
            }

            return View(insuranceType);
        }

        // GET: InsuranceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceTypeID,Title,Price")] InsuranceType insuranceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceType);
        }

        // GET: InsuranceType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceType.FindAsync(id);
            if (insuranceType == null)
            {
                return NotFound();
            }
            return View(insuranceType);
        }

        // POST: InsuranceType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceTypeID,Title,Price")] InsuranceType insuranceType)
        {
            if (id != insuranceType.InsuranceTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceTypeExists(insuranceType.InsuranceTypeID))
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
            return View(insuranceType);
        }

        // GET: InsuranceType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceType
                .FirstOrDefaultAsync(m => m.InsuranceTypeID == id);
            if (insuranceType == null)
            {
                return NotFound();
            }

            return View(insuranceType);
        }

        // POST: InsuranceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceType = await _context.InsuranceType.FindAsync(id);
            _context.InsuranceType.Remove(insuranceType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceTypeExists(int id)
        {
            return _context.InsuranceType.Any(e => e.InsuranceTypeID == id);
        }
    }
}
