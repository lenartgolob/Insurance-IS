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
    public class InsuranceSubtypesController : Controller
    {
        private readonly InsuranceContext _context;

        public InsuranceSubtypesController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: InsuranceSubtypes
        public async Task<IActionResult> Index()
        {
            var insuranceSubtypes = _context.InsuranceSubtype
                .Include(i => i.InsuranceType)
                .AsNoTracking();
            return View(await insuranceSubtypes.ToListAsync());

        }

        // GET: InsuranceSubtypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubtype = await _context.InsuranceSubtype
                .Include(s => s.InsuranceType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubtypeID == id);
            if (insuranceSubtype == null)
            {
                return NotFound();
            }

            return View(insuranceSubtype);
        }

        // GET: InsuranceSubtypes/Create
        public IActionResult Create()
        {
            PopulateInsuranceTypesDropDownList();
            return View();
        }

        // POST: InsuranceSubtypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceSubtypeID,Title,Rate,Description,InsuranceTypeID")] InsuranceSubtype insuranceSubtype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceSubtype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // PopulateInsuranceTypesDropDownList(insuranceSubtype.InsuranceTypeFKID);
            PopulateInsuranceTypesDropDownList(insuranceSubtype.InsuranceTypeID);
            return View(insuranceSubtype);
        }

        // GET: InsuranceSubtypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var insuranceSubtype = await _context.InsuranceSubtype.FindAsync(id);
            var insuranceSubtype = await _context.InsuranceSubtype
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubtypeID == id);
            if (insuranceSubtype == null)
            {
                return NotFound();
            }
            PopulateInsuranceTypesDropDownList(insuranceSubtype.InsuranceTypeID);
            // PopulateInsuranceTypesDropDownList(insuranceSubtype.InsuranceTypeFKID);
            return View(insuranceSubtype);
        }

        // POST: InsuranceSubtypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceSubtypeID,Title,Rate,Description,InsuranceTypeID")] InsuranceSubtype insuranceSubtype)
        {
            if (id != insuranceSubtype.InsuranceSubtypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceSubtype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceSubtypeExists(insuranceSubtype.InsuranceSubtypeID))
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
            return View(insuranceSubtype);
        }

        private void PopulateInsuranceTypesDropDownList(object selectedInsuranceType = null)
        {
            var insuranceTypesQuery = from t in _context.InsuranceType
                                orderby t.Title
                                select t;
            // ViewBag.InsuranceTypeFKID = new SelectList(insuranceTypesQuery.AsNoTracking(), "InsuranceTypeID", "Title", selectedInsuranceType);
            ViewBag.InsuranceTypeID = new SelectList(insuranceTypesQuery.AsNoTracking(), "InsuranceTypeID", "Title", selectedInsuranceType);

        }

        // GET: InsuranceSubtypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceSubtype = await _context.InsuranceSubtype
                .Include(s => s.InsuranceType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsuranceSubtypeID == id);
            if (insuranceSubtype == null)
            {
                return NotFound();
            }

            return View(insuranceSubtype);
        }

        // POST: InsuranceSubtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceSubtype = await _context.InsuranceSubtype.FindAsync(id);
            _context.InsuranceSubtype.Remove(insuranceSubtype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceSubtypeExists(int id)
        {
            return _context.InsuranceSubtype.Any(e => e.InsuranceSubtypeID == id);
        }
    }
}