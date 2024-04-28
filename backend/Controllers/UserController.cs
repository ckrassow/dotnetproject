using EuroPredApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroPredApi.Data;
using EuroPredApi.Services;
using EuroPredApi.DTOs;
using EuroPredApi.Types;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace EuroPredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher _passwordHasher;
        private readonly TokenService _tokenService;

        public UserController(AppDbContext context, PasswordHasher passwordHasher, TokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {

                return NotFound();
            }

            var FavouriteTeam = await _context.NationalTeams.FirstOrDefaultAsync(team => team.Id == user.NationalTeamId);
            string FavouriteTeamName = "";
            if (FavouriteTeam != null) {

                FavouriteTeamName = FavouriteTeam.Name;
            } 

            var team = await _context.Teams.FirstOrDefaultAsync(team => team.Id == user.TeamId);

            var userProfileDTO = new UserProfileDTO {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FavouriteTeam = FavouriteTeamName,
                Team = team
            };

            return userProfileDTO;
        }

        [HttpGet("{id}/playerpredictions")]
        public async Task<ActionResult<List<PlayerPrediction>>> GetPlayerPrediction(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {

                return NotFound();
            }

            var userPlayerPreds = _context.UserPlayerPredictions
                .Where(up => up.UserId == id)
                .Include(up => up.Prediction.Player)
                .Select(up => up.Prediction)
                .ToList();
            
            return Ok(userPlayerPreds);
        }

        [HttpGet("{id}/teampredictions")]
        public async Task<ActionResult<List<TeamPrediction>>> GetTeamPredictions(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {

                return NotFound();
            }

            var userTeamPreds = _context.UserTeamPredictions
                .Where(up => up.UserId == id)
                .Include(up => up.Prediction.NationalTeam)
                .Select(up => up.Prediction)
                .ToList();
            
            return Ok(userTeamPreds);
        }

        [HttpGet("{id}/tournamentpredictions")]
        public async Task<ActionResult<List<TournamentPrediction>>> GetTournamentPredictions(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {

                return NotFound();
            }

            var userTournamentPreds = _context.UserTournamentPredictions
                .Where(up => up.UserId == id)
                .Select(up => up.Prediction)
                .ToList();
            
            return Ok(userTournamentPreds);
        }

        [HttpPut("{id}/playerprediction/{predId}")]
        [Authorize]
        public async Task<ActionResult<List<UserPrediction<PlayerPrediction>>>> UpdatePlayerPrediction(int id, int predId, PlayerPredictionUpdateDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                
                return NotFound();
            }

            var player = await _context.Players.FindAsync(dto.PlayerId);
            if (player == null) {
                
                return BadRequest("Player doesn't exist");
            }

            var userPrediction = await _context.UserPlayerPredictions
                .Include(up => up.Prediction)
                .FirstOrDefaultAsync(up => up.UserId == id && up.PredictionId == predId);
            
            userPrediction.Prediction.PlayerId = dto.PlayerId;
            userPrediction.Prediction.Player = player;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/teamprediction/{predId}")]
        [Authorize]
        public async Task<ActionResult<List<UserPrediction<TeamPrediction>>>> UpdateTeamPrediction(int id, int predId, NationalTeamPredictionUpdateDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                
                return NotFound();
            }

            var nationalTeam = await _context.NationalTeams.FindAsync(dto.NationalTeamId);
            if (nationalTeam == null) {
                
                return BadRequest("Team doesn't exist");
            }

            var userPrediction = await _context.UserTeamPredictions
                .Include(up => up.Prediction)
                .FirstOrDefaultAsync(up => up.UserId == id && up.PredictionId == predId);
            
            userPrediction.Prediction.NationalTeamId = dto.NationalTeamId;
            userPrediction.Prediction.NationalTeam = nationalTeam;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/tournamentprediction/{predId}")]
        [Authorize]
        public async Task<ActionResult<List<UserPrediction<TournamentPrediction>>>> UpdateTournamentPrediction(int id, int predId, TournamentPredictionUpdateDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                
                return NotFound();
            }

            var userPrediction = await _context.UserTournamentPredictions
                .Include(up => up.Prediction)
                .FirstOrDefaultAsync(up => up.UserId == id && up.PredictionId == predId);
            
            userPrediction.Prediction.PredictionValue = dto.PredictionValue;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }
    
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO registerDTO)
        {   
            Console.WriteLine($"Received username: {registerDTO.Username}");
            if (string.IsNullOrEmpty(registerDTO.Username)) {
                
                return BadRequest("Username is required");
            }

            if (string.IsNullOrEmpty(registerDTO.Password) || registerDTO.Password.Length < 8) {

                return BadRequest("Password must be at least 8 characters long");
            }

            var existingUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username == registerDTO.Username);
            if (existingUsername != null) {

                return BadRequest("Username is already in use");
            }

            var user = new User {

                Username = registerDTO.Username,
                PasswordHash = _passwordHasher.HashPassword(registerDTO.Password),
                UserPlayerPredictions = new List<UserPrediction<PlayerPrediction>>(),
                UserTeamPredictions = new List<UserPrediction<TeamPrediction>>(),
                UserTournamentPredictions = new List<UserPrediction<TournamentPrediction>>()
            };

            foreach (PlayerPredictionType predictionType in Enum.GetValues(typeof(PlayerPredictionType)))
            {
                user.UserPlayerPredictions.Add(new UserPrediction<PlayerPrediction>
                {
                    User = user,
                    Prediction = new PlayerPrediction { PredictionType = predictionType },
                    PredictionTypeString = predictionType.ToString()
                });
            }

            foreach (TeamPredictionType predictionType in Enum.GetValues(typeof(TeamPredictionType)))
            {
                user.UserTeamPredictions.Add(new UserPrediction<TeamPrediction>
                {
                    User = user,
                    Prediction = new TeamPrediction { PredictionType = predictionType },
                    PredictionTypeString = predictionType.ToString()
                });
            }

            foreach (TournamentPredictionType predictionType in Enum.GetValues(typeof(TournamentPredictionType)))
            {
                user.UserTournamentPredictions.Add(new UserPrediction<TournamentPrediction>
                {
                    User = user,
                    Prediction = new TournamentPrediction { PredictionType = predictionType },
                    PredictionTypeString = predictionType.ToString()
                });
            }

            try {

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex) {
                
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occured while registering the user");
            }
            
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(RegisterDTO registerDTO)
        {   
            Console.WriteLine($"Received username: {registerDTO.Username}");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == registerDTO.Username);
            if (user == null)
                return NotFound();
            if (!_passwordHasher.VerifyPassword(registerDTO.Password, user.PasswordHash))
                return Unauthorized();
            Console.WriteLine($"Username: {user.Id}");
            var token = _tokenService.GenerateToken(user);
            user.RefreshToken = GenerateRefreshToken();
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(new { token, refreshToken = user.RefreshToken, userId = user.Id });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null) {

                return Unauthorized();
            }

            var newJwtToken = _tokenService.GenerateToken(user);
            user.RefreshToken = GenerateRefreshToken();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { token = newJwtToken, refreshToken = user.RefreshToken});
        }

        private string GenerateRefreshToken() {

            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }

    
}

