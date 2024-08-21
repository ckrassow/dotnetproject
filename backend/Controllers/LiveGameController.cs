using Microsoft.AspNetCore.Mvc;
using EuroPredApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EuroPredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LiveGameController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LiveGameController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiveGameDTO>>> GetLiveGames()
        {
            try
            {
                var games = await _context.Games
                    .Where(g => g.Stage == "GROUP_STAGE")
                    .Include(g => g.Score)
                    .ThenInclude(s => s.FullTime)
                    .Include(g => g.Score)
                    .ThenInclude(s => s.HalfTime)
                    .ToListAsync();
                    
                var gameDTOs = games.Select(game => new LiveGameDTO
                {   
                    Id = game.Id,
                    Status = game.Status,
                    UtcDate = game.UtcDate,
                    Matchday = game.Matchday,
                    Stage = game.Stage,
                    HomeTeam = game.HomeTeam,
                    AwayTeam = game.AwayTeam,
                    Winner = game.Score.Winner,
                    Group = game.Group,
                    FullTimeScore = new FullTimeDTO { Home = game.Score.FullTime.Home, Away = game.Score.FullTime.Away },
                    HalfTimeScore = new HalfTimeDTO { Home = game.Score.HalfTime.Home, Away = game.Score.HalfTime.Away },
                    LastUpdated = game.LastUpdated
                }).ToList();

                return Ok(gameDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while fetching live games");
            }
        }

    }

}