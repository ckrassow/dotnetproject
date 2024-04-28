using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EuroPredApi.Models;
using Microsoft.EntityFrameworkCore;
using EuroPredApi.Data;
using Microsoft.AspNetCore.Authorization;
using EuroPredApi.DTOs;

namespace EuroPredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalTeamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NationalTeamController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NationalTeamDTO>>> GetNationalTeams()
        {   
            return await _context.NationalTeams.Select(nt => new NationalTeamDTO {
                Id = nt.Id,
                Name = nt.Name,
                PlayoffAppearences = nt.PlayoffAppearences,
                FifaRanking = nt.FifaRanking,
                Group = nt.Group,
                ImagePath = nt.ImagePath,
            }).ToListAsync();
        }

        [HttpGet("{teamName}")]
        public async Task<ActionResult<NationalTeam>> GetTeamByName(string teamName)
        {   
            Console.WriteLine(teamName);
            var nationalTeam = await _context.NationalTeams
                .Include(nt => nt.Players)
                .FirstOrDefaultAsync(nt => nt.Name == teamName);

            if (nationalTeam == null)
            {
                return NotFound();
            }

            return Ok(nationalTeam);
        }

        [HttpGet("{teamName}/players")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayersByTeam(string teamName)
        {   
            Console.WriteLine(teamName);
            var nationalTeam = await _context.NationalTeams
                .Include(nt => nt.Players)
                .FirstOrDefaultAsync(nt => nt.Name == teamName);

            if (nationalTeam == null)
            {
                return NotFound(); 
            }

            var playerDtos = nationalTeam.Players.Select(player => new PlayerDTO {
                Id = player.Id,
                No = player.No,
                Pos = player.Pos,
                Name = player.Name,
                Age = player.Age,
                Caps = player.Caps,
                Goals = player.Goals,
                Club = player.Club,
                NationalTeamId = player.NationalTeamId,
                ImagePath = player.ImagePath
            });

            return Ok(playerDtos); 
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