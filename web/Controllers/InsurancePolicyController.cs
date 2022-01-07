using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using SelectPdf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace web.Controllers
{
    [Authorize(Roles = "Administrator, Agent")]
    public class InsurancePolicyController : Controller
    {
        private readonly InsuranceContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public InsurancePolicyController(InsuranceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        [HttpPost]
        public IActionResult GeneratePdf()
        {
            string baseUrl = string.Format("{0}://{1}", 
                       HttpContext.Request.Scheme, HttpContext.Request.Host);

            var html = Request.Form["html"].ToString();
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");

            HtmlToPdf oHtmlToPdf = new HtmlToPdf();
            PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html, baseUrl);
            byte[] pdf = oPdfDocument.Save();
            oPdfDocument.Close();

            return File(
                pdf,
                "application/pdf",
                "InsurancePolicy.pdf"
            );
        }

        // GET: InsurancePolicy
        public async Task<IActionResult> Index()
        {
            var insurancePolicies = _context.InsurancePolicy
                .Include(i => i.InsuranceSubject)
                .Include(i => i.Insured)
                .Include(i => i.InsuranceSubtype)
                .AsNoTracking();
            return View(await insurancePolicies.ToListAsync());
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
                .Include(p => p.InsuranceSubject)
                .Include(p => p.InsuranceSubtype)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InsurancePolicyID == id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }
            var users = await _usermanager.Users.ToListAsync();
            foreach (ApplicationUser user in users)
            {
                if(String.Equals(user.Id, insurancePolicy.OwnerID)){
                    ViewData["Owner"] = user.FirstName + " " + user.LastName;
                }
            }
            string[] lines = insurancePolicy.InsuranceSubject.Description.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            ViewData["Description"] = lines;
            ViewData["VAT"] = insurancePolicy.FinalSum * (decimal)0.22;
            ViewData["withoutVAT"] = insurancePolicy.FinalSum - insurancePolicy.FinalSum * (decimal)0.22;
            return View(insurancePolicy);
        }

        // GET: InsurancePolicy/Create
        public IActionResult Create()
        {
            PopulateInsuredDropDownList();
            PopulateSubjectsDropDownList();
            PopulateSubtypesDropDownList();
            return View();
        }

        // POST: InsurancePolicy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsurancePolicyID,DateFrom,DateTo,InsuredID,InsuranceSubjectID,InsuranceSubtypeID")] InsurancePolicy insurancePolicy)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var query = from s in _context.InsuranceSubject
                            where s.InsuranceSubjectID == insurancePolicy.InsuranceSubjectID
                            select s;
                var subject = query.FirstOrDefault<InsuranceSubject>();
                var query2 = from s in _context.InsuranceSubtype
                            where s.InsuranceSubtypeID == insurancePolicy.InsuranceSubtypeID
                            select s;
                var subtype = query2.FirstOrDefault<InsuranceSubtype>();
                
                // ((vrednost objekta * rate)/365)*trajanje zavarovalne police
                insurancePolicy.FinalSum = ((subject.EstimatedValue*subtype.Rate)/365)*(decimal)((insurancePolicy.DateTo - insurancePolicy.DateFrom).TotalDays);
                insurancePolicy.OwnerID = currentUser.Id;
                _context.Add(insurancePolicy);
                await _context.SaveChangesAsync();        
                return RedirectToAction(nameof(Index));
            }
            PopulateInsuredDropDownList(insurancePolicy.InsuredID);
            PopulateSubjectsDropDownList(insurancePolicy.InsuranceSubjectID);
            PopulateSubtypesDropDownList(insurancePolicy.InsuranceSubtypeID);

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
            PopulateSubjectsDropDownList(insurancePolicy.InsuranceSubjectID);
            PopulateSubtypesDropDownList(insurancePolicy.InsuranceSubtypeID);

            return View(insurancePolicy);
        }

        // POST: InsurancePolicy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsurancePolicyID,FinalSum,DateFrom,DateTo,InsuredID,InsuranceSubjectID,InsuranceSubtypeID")] InsurancePolicy insurancePolicy)
        {
            if (id != insurancePolicy.InsurancePolicyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var query = from s in _context.InsuranceSubject
                                where s.InsuranceSubjectID == insurancePolicy.InsuranceSubjectID
                                select s;
                    var subject = query.FirstOrDefault<InsuranceSubject>();
                    var query2 = from s in _context.InsuranceSubtype
                                where s.InsuranceSubtypeID == insurancePolicy.InsuranceSubtypeID
                                select s;
                    var subtype = query2.FirstOrDefault<InsuranceSubtype>();

                    var currentUser = await _usermanager.GetUserAsync(User);
                    insurancePolicy.OwnerID = currentUser.Id;

                    // ((vrednost objekta * rate)/365)*trajanje zavarovalne police
                    insurancePolicy.FinalSum = ((subject.EstimatedValue*subtype.Rate)/365)*(decimal)((insurancePolicy.DateTo - insurancePolicy.DateFrom).TotalDays);
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
            //     p => p.FinalSum, p => p.InsuredID, p => p.DateFrom, p => p.DatoTo, p => InsuranceSubjectID, p => InsuranceSubtypeID))
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
            // PopulateSubjectsDropDownList(insurancePolicy.InsuranceSubjectID);
            // PopulateSubtypesDropDownList(insurancePolicy.InsuranceSubtypeID);
            // return View(insurancePolicyToUpdate);
        }

        private void PopulateInsuredDropDownList(object selectedInsured = null)
        {
            var insuredQuery = from i in _context.Insured
                                orderby i.FullName
                                select i;
            ViewBag.InsuredID = new SelectList(insuredQuery.AsNoTracking(), "ID", "FullName", selectedInsured);
        }

        private void PopulateSubjectsDropDownList(object selectedSubject = null)
        {
            var subjectQuery = from i in _context.InsuranceSubject
                                orderby i.Title
                                select i;
            ViewBag.InsuranceSubjectID = new SelectList(subjectQuery.AsNoTracking(), "InsuranceSubjectID", "Title", selectedSubject);
            ViewData["subjectSize"] = subjectQuery.Count();
            ViewData["InsuranceSubject"] = Newtonsoft.Json.JsonConvert.SerializeObject(subjectQuery);
        }

        private void PopulateSubtypesDropDownList(object selectedSubtype = null)
        {
            var subtypeQuery = from i in _context.InsuranceSubtype
                                orderby i.Title
                                select i;
            var joinTypeQuery = from s in _context.InsuranceSubject
                join st in _context.InsuranceSubjectType on s.InsuranceSubjectTypeID equals st.InsuranceSubjectTypeID
                join t in _context.InsuranceType on st.InsuranceTypeID equals t.InsuranceTypeID
                orderby s.Title
                select new {s.InsuranceSubjectID, s.Title, st.InsuranceSubjectTypeID, t.InsuranceTypeID};
            ViewData["InsuranceSubtype"] = Newtonsoft.Json.JsonConvert.SerializeObject(subtypeQuery);     
            ViewData["joinSubtype"] = Newtonsoft.Json.JsonConvert.SerializeObject(joinTypeQuery);   
            ViewData["joinSize"] = joinTypeQuery.Count();
            ViewData["subtypesSize"] = subtypeQuery.Count();

            ViewBag.InsuranceSubtypeID = new SelectList(subtypeQuery.AsNoTracking(), "InsuranceSubtypeID", "Title", selectedSubtype);
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
                .Include(p => p.InsuranceSubject)
                .Include(p => p.InsuranceSubtype)
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