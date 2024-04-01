using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroPredApi.Models;
using EuroPredApi.Data;

namespace EuroPredApi.Controllers
{
    [Route("europredapi/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase {

        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context) {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers() {

            return await _context.Players.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id) {
             
             var player = await _context.Players.FindAsync(id);
             if (player == null) {

                return NotFound();
             }

             return player;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player) {

            if (id != player.Id) {

                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try {

                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException) {

                if (!PlayerExists(id)) {

                    return NotFound();
                }

                else {
                    
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player) {

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id, }, player);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id) {

            var player = await _context.Players.FindAsync(id);
            if (player == null) {

                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id) {

            return _context.Players.Any(e => e.Id == id);
        }


    }
}