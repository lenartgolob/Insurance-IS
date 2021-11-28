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
    public class InsurancePolicyController : Controller
    {
        private readonly InsuranceContext _context;

        public InsurancePolicyController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: InsurancePolicy
        public async Task<IActionResult> Index()
        {
            return View(await _context.InsurancePolicy.ToListAsync());
        }

        // GET: InsurancePolicy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurancePolicy = await _context.InsurancePolicy
                .Include(p => p.Insured)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsurancePolicyID == id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }

            return View(insurancePolicy);
        }

        // GET: InsurancePolicy/Create
        public IActionResult Create()
        {
            PopulateInsuredDropDownList();
            return View();
        }

        // POST: InsurancePolicy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsurancePolicyID,InsuredID,FinalSum,DateFrom,DateTo")] InsurancePolicy insurancePolicy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurancePolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateInsuredDropDownList(insurancePolicy.InsuredID);
            return View(insurancePolicy);
        }

        // GET: InsurancePolicy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurancePolicy = await _context.InsurancePolicy
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsurancePolicyID == id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }
            PopulateInsuredDropDownList(insurancePolicy.InsuredID);
            return View(insurancePolicy);
        }

        // POST: InsurancePolicy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsurancePolicyID,FinalSum,DateFrom,DateTo")] InsurancePolicy insurancePolicy)
        {
            if (id != insurancePolicy.InsurancePolicyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurancePolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsurancePolicyExists(insurancePolicy.InsurancePolicyID))
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
            return View(insurancePolicy);
            // var insurancePolicyToUpdate = await _context.InsurancePolicy
            //     .FirstOrDefaultAsync(p => p.InsurancePolicyID == id);

            // if (await TryUpdateModelAsync<InsurancePolicy>(insurancePolicyToUpdate,
            //     "",
            //     p => p.FinalSum, p => p.InsuredID, p => p.DateFrom, p => p.DatoTo))
            // {
            //     try
            //     {
            //         await _context.SaveChangesAsync();
            //     }
            //     catch (DbUpdateException /* ex */)
            //     {
            //         //Log the error (uncomment ex variable name and write a log.)
            //         ModelState.AddModelError("", "Unable to save changes. " +
            //             "Try again, and if the problem persists, " +
            //             "see your system administrator.");
            //     }
            //     return RedirectToAction(nameof(Index));
            // }
            // PopulateInsuredDropDownList(insurancePolicy.InsuredID);
            // return View(insurancePolicyToUpdate);
        }

        private void PopulateInsuredDropDownList(object selectedInsured = null)
        {
            var insuredQuery = from i in _context.Insured
                                orderby i.LastName
                                select i;
            ViewBag.InsuredID = new SelectList(insuredQuery.AsNoTracking(), "ID", "FullName", selectedInsured);
        }

        // GET: InsurancePolicy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurancePolicy = await _context.InsurancePolicy
                .Include(p => p.Insured)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsurancePolicyID == id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }

            return View(insurancePolicy);
        }

        // POST: InsurancePolicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurancePolicy = await _context.InsurancePolicy.FindAsync(id);
            _context.InsurancePolicy.Remove(insurancePolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsurancePolicyExists(int id)
        {
            return _context.InsurancePolicy.Any(e => e.InsurancePolicyID == id);
        }
    }
}
