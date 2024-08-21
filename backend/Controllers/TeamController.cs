using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroPredApi.Models;
using EuroPredApi.Data;
using EuroPredApi.DTOs;
using EuroPredApi.Utils;
using EuroPredApi.Types;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EuroPredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TeamController : ControllerBase {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context) {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PredictionTeam>>> GetTeams() {

            return await _context.PredictionTeams.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PredictionTeam>> GetTeam(int id)
        {
            var team = await _context.PredictionTeams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PredictionTeam>> RegisterTeam(RegisterTeamDTO registerTeamDTO)
        {   
            if (string.IsNullOrEmpty(registerTeamDTO.Name)) {

                return BadRequest("Name is required");
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            var captain = await _context.Users.FindAsync(userIdFromToken);
            
            if (captain == null) {

                return BadRequest("User for Captain does not exist");
            }

            bool isMemberOfAnyTeam = await _context.Members.AnyAsync(m => m.TeamMemberId == userIdFromToken);
            if (isMemberOfAnyTeam)
            {
                return Conflict("User can only be part of one team.");
            }

            var team = new PredictionTeam {
                Name = registerTeamDTO.Name,
                TeamPlayerPredictions = new List<TeamPrediction<PlayerPrediction>>(),
                TeamNationalTeamPredictions = new List<TeamPrediction<NationalTeamPrediction>>(),
                TeamTournamentPredictions = new List<TeamPrediction<TournamentPrediction>>(),
                Members = new List<Member>()
            };

            var newMember = new Member
            {
                TeamMemberId = captain.Id,
                TeamMember = captain,
                IsCaptain = true,
                Team = team
            };

            team.Members.Add(newMember);

            foreach (PlayerPredictionType predictionType in Enum.GetValues(typeof(PlayerPredictionType)))
            {
                team.TeamPlayerPredictions.Add(new TeamPrediction<PlayerPrediction>
                {
                    Team = team,
                    Prediction = new PlayerPrediction { PredictionType = predictionType },
                    PredictionTypeString = predictionType.ToString()
                });
            }

            foreach (TeamPredictionType predictionType in Enum.GetValues(typeof(TeamPredictionType)))
            {
                team.TeamNationalTeamPredictions.Add(new TeamPrediction<NationalTeamPrediction>
                {
                    Team = team,
                    Prediction = new NationalTeamPrediction { PredictionType = predictionType },
                    PredictionTypeString = predictionType.ToString()
                });
            }

            foreach (TournamentPredictionType predictionType in Enum.GetValues(typeof(TournamentPredictionType)))
            {
                team.TeamTournamentPredictions.Add(new TeamPrediction<TournamentPrediction>
                {
                    Team = team,
                    Prediction = new TournamentPrediction { PredictionType = predictionType },
                    PredictionTypeString = predictionType.ToString()
                });
            }

            try {

                await _context.PredictionTeams.AddAsync(team);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex) {
                
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occured while registering the team");
            }

            _context.PredictionTeams.Add(team);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPut("{id}/captain")]
        [Authorize]
        public async Task<IActionResult> PutTeam(int id, UpdateCaptainDTO updateData)
        {   
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != updateData.CaptainId)
            {
                return Forbid("User not authorized to change captain"); 
            }

            var team = await _context.PredictionTeams.FindAsync(id);

            if (team == null)
            {
                return NotFound("Team not found");
            }

            var newCaptainUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == updateData.NewCaptainUsername);
            
            if (newCaptainUser == null)
            {
                return NotFound("New captain user not found");
            }

            var currentCaptain = team.Members.FirstOrDefault(m => m.IsCaptain);

            if (currentCaptain == null) 
            {
                return NotFound("Could not found current captain");
            }

            currentCaptain.IsCaptain = false;
            
            var newCaptain = team.Members.FirstOrDefault(m => m.Id == newCaptainUser.Id);
            if (newCaptain == null)
            {
                return NotFound("Member not found in team");
            }

            newCaptain.IsCaptain = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/addmember")]
        [Authorize]
        public async Task <IActionResult> AddMember(int id, UpdateMemberDTO updateData)
        {   

            DateTime date = new DateTime(2024, 06, 14);
            if (date < DateTime.Now)
            {
                return BadRequest("Passed deadline");
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != updateData.MemberId)
            {
                return Forbid("User not authorized to accept request to join team"); 
            }

            var team = await _context.PredictionTeams.FindAsync(id); 

            if (team == null)
            {
                return NotFound("Team not found");
            }
            
            if (team.Members.Count >= 4)
            {
                return Conflict("Amount of members at max limit");
            }

            var user = await _context.Users.FindAsync(updateData.MemberId);

            if (user == null)
            {
                return NotFound("Member not found");
            }

            var isUserInTeam = await _context.Members
                .AnyAsync(m => m.TeamId == id && m.TeamMemberId == user.Id);
            
            if (isUserInTeam)
            {
                return Conflict("User is already part of a team");
            }

            var newMember = new Member 
            {
                TeamId = id,
                Team = team,
                TeamMemberId = user.Id,
                TeamMember = user,
                IsCaptain = false
            };

            team.Members.Add(newMember);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/removemember")]
        [Authorize]
        public async Task<ActionResult> RemoveMember(int id, UpdateMemberDTO updateData)
        {
            DateTime date = new DateTime(2024, 06, 14);
            if (date < DateTime.Now)
            {
                return BadRequest("Passed deadline");
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            var team = await _context.PredictionTeams
                .Include(t => t.Members)
                .ThenInclude(m => m.TeamMember)
                .FirstOrDefaultAsync(t => t.Id == id);
            
            if (team == null)
            {
                return NotFound("Team not found");
            } 

            var captain = team.Members.FirstOrDefault(member => member.IsCaptain);

            if (userIdFromToken != captain.Id || userIdFromToken != updateData.MemberId)
            {
                return Forbid("This user is not authorized to remove the member"); 
            }

            var memberToRemove = team.Members.FirstOrDefault(m => m.TeamMemberId == updateData.MemberId);

            if (memberToRemove != null)
            {
                team.Members.Remove(memberToRemove);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound("Member was not found in members list");
            }

        }
    }
}
