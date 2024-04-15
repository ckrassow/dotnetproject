using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EuroPredApi.Models;
using Microsoft.EntityFrameworkCore;
using EuroPredApi.Data;

namespace EuroPredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalTeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NationalTeamsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalTeam>>> GetNationalTeams()
        {
            return await _context.NationalTeams.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NationalTeam>> GetNationalTeam(int id)
        {
            var nationalTeam = await _context.NationalTeams.FindAsync(id);

            if (nationalTeam == null)
            {
                return NotFound();
            }

            return nationalTeam;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNationalTeam(int id, NationalTeam nationalTeam)
        {
            if (id != nationalTeam.Id)
            {
                return BadRequest();
            }

            _context.Entry(nationalTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationalTeamExists(id))
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
        public async Task<ActionResult<NationalTeam>> PostNationalTeam(NationalTeam nationalTeam)
        {
            _context.NationalTeams.Add(nationalTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNationalTeam", new { id = nationalTeam.Id }, nationalTeam);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<NationalTeam>> DeleteNationalTeam(int id)
        {
            var nationalTeam = await _context.NationalTeams.FindAsync(id);
            if (nationalTeam == null)
            {
                return NotFound();
            }

            _context.NationalTeams.Remove(nationalTeam);
            await _context.SaveChangesAsync();

            return nationalTeam;
        }

        private bool NationalTeamExists(int id)
        {
            return _context.NationalTeams.Any(e => e.Id == id);
        }
    }
}