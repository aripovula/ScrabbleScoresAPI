using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrabbleScoreAPI.Models;

namespace ScrabbleScoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoringValueSetsController : ControllerBase
    {
        private readonly ScoringValueSetContext _context;

        public ScoringValueSetsController(ScoringValueSetContext context)
        {
            _context = context;
        }

        // GET: api/ScoringValueSets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoringValueSet>>> GetScoringValueSet()
        {
            return await _context.ScoringValueSet.ToListAsync();
        }

        // GET: api/ScoringValueSets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScoringValueSet>> GetScoringValueSet(long id)
        {
            var scoringValueSet = await _context.ScoringValueSet.FindAsync(id);

            if (scoringValueSet == null)
            {
                return NotFound();
            }

            return scoringValueSet;
        }

        // PUT: api/ScoringValueSets/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScoringValueSet(long id, ScoringValueSet scoringValueSet)
        {
            if (id != scoringValueSet.Id)
            {
                return BadRequest();
            }

            _context.Entry(scoringValueSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoringValueSetExists(id))
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

        // POST: api/ScoringValueSets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ScoringValueSet>> PostScoringValueSet(ScoringValueSet scoringValueSet)
        {
            _context.ScoringValueSet.Add(scoringValueSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScoringValueSet", new { id = scoringValueSet.Id }, scoringValueSet);
        }

        // DELETE: api/ScoringValueSets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ScoringValueSet>> DeleteScoringValueSet(long id)
        {
            var scoringValueSet = await _context.ScoringValueSet.FindAsync(id);
            if (scoringValueSet == null)
            {
                return NotFound();
            }

            _context.ScoringValueSet.Remove(scoringValueSet);
            await _context.SaveChangesAsync();

            return scoringValueSet;
        }

        private bool ScoringValueSetExists(long id)
        {
            return _context.ScoringValueSet.Any(e => e.Id == id);
        }
    }
}
