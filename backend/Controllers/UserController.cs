using EuroPredApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using EuroPredApi.Data;
using EuroPredApi.Services;
using EuroPredApi.DTOs;
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
                Email = user.Email,
                PlayerPredictions = user.PlayerPredictions,
                TeamPredictions = user.TeamPredictions,
                TournamentPredictions = user.TournamentPredictions,
                TeamId = user.TeamId,
            });

            return Ok(userDTOs);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user, string password)
        {   
            if (string.IsNullOrEmpty(user.Username)) {
                
                return BadRequest("Username is required");
            }

            if (string.IsNullOrEmpty(user.Email)) {

                return BadRequest("Email is required");
            }

            if (!IsValidEmail(user.Email)) {

                return BadRequest("Invalid email format");
            }

            if (string.IsNullOrEmpty(password) || password.Length < 8) {

                return BadRequest("Password must be at least 8 characters long");
            }

            user.PasswordHash = _passwordHasher.HashPassword(password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return NotFound();
            if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
                return Unauthorized();
            
            var token = _tokenService.GenerateToken(user);
            user.RefreshToken = GenerateRefreshToken();
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(new { token, refreshToken = user.RefreshToken });
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

        private static bool IsValidEmail(string email) {

            try {

                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }

            catch {

                return false;
            }
        }

        private string GenerateRefreshToken() {

            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }

    
}

