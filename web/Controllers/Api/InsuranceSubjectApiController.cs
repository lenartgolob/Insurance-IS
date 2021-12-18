using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/v1/InsuranceSubject")]
    [ApiController]
    public class InsuranceSubjectApiController : ControllerBase
    {
        private readonly InsuranceContext _context;

        public InsuranceSubjectApiController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: api/InsuranceSubjectApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceSubject>>> GetInsuranceSubject()
        {
            return await _context.InsuranceSubject.ToListAsync();
        }

        // GET: api/InsuranceSubjectApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceSubject>> GetInsuranceSubject(int id)
        {
            var insuranceSubject = await _context.InsuranceSubject.FindAsync(id);

            if (insuranceSubject == null)
            {
                return NotFound();
            }

            return insuranceSubject;
        }

        // PUT: api/InsuranceSubjectApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsuranceSubject(int id, InsuranceSubject insuranceSubject)
        {
            if (id != insuranceSubject.InsuranceSubjectID)
            {
                return BadRequest();
            }

            _context.Entry(insuranceSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceSubjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InsuranceSubjectApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InsuranceSubject>> PostInsuranceSubject(InsuranceSubject insuranceSubject)
        {
            _context.InsuranceSubject.Add(insuranceSubject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsuranceSubject", new { id = insuranceSubject.InsuranceSubjectID }, insuranceSubject);
        }

        // DELETE: api/InsuranceSubjectApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsuranceSubject(int id)
        {
            var insuranceSubject = await _context.InsuranceSubject.FindAsync(id);
            if (insuranceSubject == null)
            {
                return NotFound();
            }

            _context.InsuranceSubject.Remove(insuranceSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsuranceSubjectExists(int id)
        {
            return _context.InsuranceSubject.Any(e => e.InsuranceSubjectID == id);
        }
    }
}
