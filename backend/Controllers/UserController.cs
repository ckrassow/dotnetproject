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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers() {

            var users = await _context.Users.ToListAsync();
            var userDTOs = users.Select(user => new UserDTO {
                Id = user.Id,
                Username = user.Username,
                FavouriteTeam = user.FavouriteTeam,
                PlayerPredictions = user.PlayerPredictions,
                TeamPredictions = user.TeamPredictions,
                TournamentPredictions = user.TournamentPredictions,
                TeamId = user.TeamId,
            });

            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUser(int id) {

            var user = await _context.Users
                .Include(u => u.PlayerPredictions)
                .Include(u => u.TeamPredictions)
                .Include(u => u.TournamentPredictions)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) {
                return NotFound();
            }

            var userDTO = new UserDTO {
                Id = user.Id,
                Username = user.Username,
                FavouriteTeam = user.FavouriteTeam,
                PlayerPredictions = user.PlayerPredictions,
                TeamPredictions = user.TeamPredictions,
                TournamentPredictions = user.TournamentPredictions,
                TeamId = user.TeamId,
            };

            return Ok(userDTO);
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
                PlayerPredictions = new List<PlayerPrediction>(),
                TeamPredictions = new List<TeamPrediction>(),
                TournamentPredictions = new List<TournamentPrediction>()
            };

            foreach (PlayerPredictionType predictionType in Enum.GetValues(typeof(PlayerPredictionType))) {
                
                var playerPrediction = new PlayerPrediction {
                    PredictionType = predictionType,
                    PlayerId = null,
                };

                user.PlayerPredictions.Add(playerPrediction);
            }

            foreach (TeamPredictionType predictionType in Enum.GetValues(typeof(TeamPredictionType))) {

                var teamPrediction = new TeamPrediction {
                    PredictionType = predictionType,
                    NationalTeamId = null,
                };

                user.TeamPredictions.Add(teamPrediction);
            }
            
            foreach (TournamentPredictionType predictionType in Enum.GetValues(typeof(TournamentPredictionType))) {

                var tournamentPrediction = new TournamentPrediction {
                    PredictionType = predictionType,
                    PredictionValue = "",
                };

                user.TournamentPredictions.Add(tournamentPrediction);
            }


            try {

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occured while registering the user");
            }
            
            return user;
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

