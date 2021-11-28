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
using Microsoft.AspNetCore.Identity;

namespace web.Controllers
{
    // [Authorize]
    public class InsuranceTypeController : Controller
    {
        private readonly InsuranceContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public InsuranceTypeController(InsuranceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // [Authorize(Roles = "Administrator,Staff")]
        // GET: InsuranceType
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            // ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
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

            var insuranceTypes = from t in _context.InsuranceType
                        select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                insuranceTypes = insuranceTypes.Where(t => t.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    insuranceTypes = insuranceTypes.OrderByDescending(t => t.Title);
                    break;
                // case "price_desc":
                //     insuranceTypes = insuranceTypes.OrderByDescending(t => t.Price);
                //     break;
                // case "Price":
                //     insuranceTypes = insuranceTypes.OrderBy(t => t.Price);
                //     break;
                // case "Date":
                //     insured = insured.OrderBy(i => i.EnrollmentDate);
                //     break;
                // case "date_desc":
                //     insured = insured.OrderByDescending(i => i.EnrollmentDate);
                //     break;
                default:
                    insuranceTypes = insuranceTypes.OrderBy(t => t.Title);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<InsuranceType>.CreateAsync(insuranceTypes.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        public async Task<IActionResult> Create([Bind("InsuranceTypeID,Title")] InsuranceType insuranceType)
        {
            var currentUser = await _usermanager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                insuranceType.DateCreated = DateTime.Now;
                insuranceType.DateEdited = DateTime.Now;
                insuranceType.Owner = currentUser;

                _context.Add(insuranceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceType);
        }

        // GET: InsuranceType/Edit/5 
        // [Authorize(Roles = "Administrator")]
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
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceTypeID,Title")] InsuranceType insuranceType)
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
        // [Authorize(Roles = "Administrator,Staff")]
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
