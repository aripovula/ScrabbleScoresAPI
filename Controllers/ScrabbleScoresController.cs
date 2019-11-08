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
    [Route("api/ScrabbleScores")]
    [ApiController]
    public class ScrabbleScoresController : ControllerBase
    {
        private readonly ScrabbleScoreContext _context;
        private readonly ScoringValueSetContext _setContext;

        public ScrabbleScoresController(ScrabbleScoreContext context, ScoringValueSetContext setContext)
        {
            _context = context;
            _setContext = setContext;
        }

        // GET: api/ScrabbleScores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScrabbleScores()
        {
            return await _context.ScrabbleScores.ToListAsync();
        }

        // GET: api/ScrabbleScores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(long id)
        {
            var score = await _context.ScrabbleScores.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }


        // GET: api/ScrabbleScores/word/setId
        [HttpGet("getscore")]
        public ActionResult<Score> GetScoreForParams(string word, long setId)
        {
            int theScore;
            if (setId != 0) {
                var ScoringValueSet = _setContext.ScoringValueSet.Find(setId);
                theScore = ScrabbleScore.Score(word, ScoringValueSet);
            } else {
                theScore = ScrabbleScore.Score(word);
            }
            var score = new Score{ ScrabbleScore = theScore, Word = word, ScoringValueSetId = setId };

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }

        // PUT: api/ScrabbleScores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(long id, Score score)
        {
            if (id != score.Id)
            {
                return BadRequest();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
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

        // POST: api/ScrabbleScores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        {

            var ScoringValueSet = _setContext.ScoringValueSet.Find(score.ScoringValueSetId);
            var theScore = ScrabbleScore.Score(score.Word, ScoringValueSet);
            if (score.ScrabbleScore == 0) {
                score = new Score{ ScrabbleScore = theScore, Word = score.Word, ScoringValueSetId = score.ScoringValueSetId };
            } else if (score.ScrabbleScore != theScore) {
                return BadRequest("Bad request: Score is incorrect! Please double check the score.");
            }
            _context.ScrabbleScores.Add(score);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetScore", new { id = score.Id }, score);
            return CreatedAtAction(nameof(GetScore), new { id = score.Id }, score);
        }

        // DELETE: api/ScrabbleScores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Score>> DeleteScore(long id)
        {
            var score = await _context.ScrabbleScores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            _context.ScrabbleScores.Remove(score);
            await _context.SaveChangesAsync();

            return score;
        }

        private bool ScoreExists(long id)
        {
            return _context.ScrabbleScores.Any(e => e.Id == id);
        }
    }
}
