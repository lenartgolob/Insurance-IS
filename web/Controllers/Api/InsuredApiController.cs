using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Filters;

namespace web.Controllers_Api
{
    [Route("api/v1/Insured")]
    [ApiController]
    [ApiKeyAuth]
    public class InsuredApiController : ControllerBase
    {
        private readonly InsuranceContext _context;

        public InsuredApiController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: api/InsuredApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insured>>> GetInsured()
        {
            return await _context.Insured.ToListAsync();
        }

        // GET: api/InsuredApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Insured>> GetInsured(int id)
        {
            var insured = await _context.Insured.FindAsync(id);

            if (insured == null)
            {
                return NotFound();
            }

            return insured;
        }

        // PUT: api/InsuredApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsured(int id, Insured insured)
        {
            if (id != insured.ID)
            {
                return BadRequest();
            }

            _context.Entry(insured).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuredExists(id))
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

        // POST: api/InsuredApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Insured>> PostInsured(Insured insured)
        {
            _context.Insured.Add(insured);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsured", new { id = insured.ID }, insured);
        }

        // DELETE: api/InsuredApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsured(int id)
        {
            var insured = await _context.Insured.FindAsync(id);
            if (insured == null)
            {
                return NotFound();
            }

            _context.Insured.Remove(insured);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsuredExists(int id)
        {
            return _context.Insured.Any(e => e.ID == id);
        }
    }
}
