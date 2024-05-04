using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroPredApi.Models;
using EuroPredApi.Data;

namespace EuroPredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentPredictionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TournamentPredictionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentPrediction>>> GetTournamentPredictions()
        {
            return await _context.TournamentPredictions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentPrediction>> GetTournamentPrediction(int id)
        {
            var prediction = await _context.TournamentPredictions.FindAsync(id);

            if (prediction == null)
            {
                return NotFound();
            }

            return prediction;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentPrediction(int id, TournamentPrediction prediction)
        {
            if (id != prediction.Id)
            {
                return BadRequest();
            }

            _context.Entry(prediction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentPredictionExists(id))
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

        [HttpPost]
        public async Task<ActionResult<TournamentPrediction>> PostTournamentPrediction(TournamentPrediction prediction)
        {
            _context.TournamentPredictions.Add(prediction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTournamentPrediction), new { id = prediction.Id }, prediction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentPrediction(int id)
        {
            var prediction = await _context.TournamentPredictions.FindAsync(id);
            if (prediction == null)
            {
                return NotFound();
            }

            _context.TournamentPredictions.Remove(prediction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TournamentPredictionExists(int id)
        {
            return _context.TournamentPredictions.Any(e => e.Id == id);
        }
    }
}