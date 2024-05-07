using EuroPredApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroPredApi.Data;
using EuroPredApi.Services;
using EuroPredApi.DTOs;
using EuroPredApi.Types;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Security.Claims;

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

        [HttpGet("{username}")]
        public async Task<ActionResult<UserProfileDTO>> GetUserByUsername(string username)
        {   
            Console.WriteLine(username);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) {

                return NotFound();
            }

            var FavouriteTeam = await _context.NationalTeams.FirstOrDefaultAsync(team => team.Id == user.NationalTeamId);
            string FavouriteTeamName = "";
            int FavouriteTeamId = 0;
            if (FavouriteTeam != null) {

                FavouriteTeamName = FavouriteTeam.Name;
                FavouriteTeamId = FavouriteTeam.Id;
            } 

            var team = await _context.Teams.FirstOrDefaultAsync(team => team.Id == user.TeamId);

            var userProfileDTO = new UserProfileDTO {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FavouriteTeam = FavouriteTeamName,
                FavouriteTeamId = FavouriteTeamId,
                ProfilePicRef = user.ProfilePicRef,
                Team = team,
            };

            return userProfileDTO;
        }

        [HttpGet("{username}/comments")]
        public async Task<IActionResult> GetComments(string username)
        {
            var user = await _context.Users
                .Include(u => u.CommentsReceived)
                    .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) {

                return NotFound();
            }

            Console.WriteLine(user.Username);

            if (user.CommentsReceived == null) {

                return Ok(new List<CommentsDTO>());
            }

            var userCommentsReceived = user.CommentsReceived;

            var commentsDto = userCommentsReceived.Select(c => new CommentsDTO 
            {
                Author = c.Author.Username,
                Recipient = c.User.Username,
                Timestamp = c.Timestamp,
                Comment = c.Text
            }).ToList();

            Console.WriteLine(commentsDto);
            
            return Ok(commentsDto);
        }

        [HttpGet("{username}/playerpredictions")]
        public async Task<ActionResult<List<PlayerPrediction>>> GetPlayerPrediction(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) {

                return NotFound();
            }

            var userPlayerPreds = _context.UserPlayerPredictions
                .Where(up => up.UserId == user.Id)
                .Include(up => up.Prediction.Player)
                .Select(up => up.Prediction)
                .ToList();
            
            return Ok(userPlayerPreds);
        }

        [HttpGet("{username}/teampredictions")]
        public async Task<ActionResult<List<TeamPrediction>>> GetTeamPredictions(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) {

                return NotFound();
            }

            var userTeamPreds = _context.UserTeamPredictions
                .Where(up => up.UserId == user.Id)
                .Include(up => up.Prediction.NationalTeam)
                .Select(up => up.Prediction)
                .ToList();
            
            return Ok(userTeamPreds);
        }

        [HttpGet("{username}/tournamentpredictions")]
        public async Task<ActionResult<List<TournamentPrediction>>> GetTournamentPredictions(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) {

                return NotFound();
            }

            var userTournamentPreds = _context.UserTournamentPredictions
                .Where(up => up.UserId == user.Id)
                .Select(up => up.Prediction)
                .ToList();
            
            return Ok(userTournamentPreds);
        }

        [HttpPut("{id}/playerprediction/{predId}")]
        [Authorize]
        public async Task<ActionResult<List<UserPrediction<PlayerPrediction>>>> UpdatePlayerPrediction(int id, int predId, PlayerPredictionUpdateDTO dto)
        {   
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != id)
            {
                return Forbid(); 
            }

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

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != id)
            {
                return Forbid(); 
            }

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

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != id)
            {
                return Forbid(); 
            }

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
            Console.WriteLine($"Userid: {user.Id}");
            var token = _tokenService.GenerateToken(user);
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(new { token, refreshToken = user.RefreshToken, userId = user.Id, username = user.Username });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow) {

                return Unauthorized();
            }

            var newJwtToken = _tokenService.GenerateToken(user);
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { token = newJwtToken, refreshToken = user.RefreshToken});
        }

        [HttpPut("{id}/profile-picture")]
        [Authorize]
        public async Task<IActionResult> UpdateProfilePicture(int id, UpdateProfilePictureDTO updateData)
        {   
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != id)
            {
                return Forbid(); 
            }

            Console.WriteLine(updateData.ProfilePicRef);
            if (updateData == null || string.IsNullOrEmpty(updateData.ProfilePicRef)) 
            {
                return BadRequest("Invalid profile picture data.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.ProfilePicRef = updateData.ProfilePicRef;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateUserProfileDTO updateData)
        {   
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != id)
            {
                return Forbid(); 
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = updateData.FirstName;
            user.LastName = updateData.LastName;
            user.FavouriteTeam = updateData.FavouriteTeam;

            if (updateData.FavouriteTeam != null) {
                user.NationalTeamId = updateData.FavouriteTeam.Id;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != id)
            {
                return Forbid(); 
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (!_passwordHasher.VerifyPassword(changePasswordDTO.OldPassword, user.PasswordHash)) 
            {
                return Unauthorized();
            }

            var newPasswordHash = _passwordHasher.HashPassword(changePasswordDTO.NewPassword);
            user.PasswordHash = newPasswordHash;
            await _context.SaveChangesAsync();

            return NoContent();       
        }

        [HttpPost("addcomment")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] AddCommentDTO addCommentDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            var user = await _context.Users.FindAsync(addCommentDTO.AuthorId);
            if (user == null)
            {
                return NotFound();
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != user.Id)
            {
                return Forbid(); 
            }

            var recipientUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == addCommentDTO.Recipient);
            if (recipientUser == null)
            {
                return NotFound();
            }
            var comment = new Comment 
            {
                Text = addCommentDTO.Text,
                Timestamp = DateTime.UtcNow,
                AuthorId = addCommentDTO.AuthorId,
                RecipientId = recipientUser.Id
            };
            if (recipientUser.CommentsReceived == null) {

                recipientUser.CommentsReceived = new List<Comment>();
            }

            recipientUser.CommentsReceived.Add(comment);
            
            if (user.CommentsWritten == null) {

                user.CommentsWritten = new List<Comment>();
            }

            user.CommentsWritten.Add(comment);

            await _context.SaveChangesAsync();

            var commentDto = new CommentsDTO
            {
                Author = user.Username,
                Recipient = recipientUser.Username,
                Timestamp = comment.Timestamp,
                Comment = comment.Text
            };

            return Ok(commentDto);
        }

        [HttpDelete("{id}/profile-picture")]
        [Authorize]
        public async Task<IActionResult> RemoveProfilePicture(int id)
        {   
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid user ID in token.");
            }

            int userIdFromToken = int.Parse(userIdClaim.Value);

            if (userIdFromToken != id)
            {
                return Forbid(); 
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.ProfilePicRef = null;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(string query)
        {   
            Console.WriteLine("Search users endpoint");
            if (string.IsNullOrEmpty(query))
            {
                return Ok(new List<UserProfileDTO>()); 
            }

            query = query.ToLower();

            List<User> users; 

            var exactMatchUser = await _context.Users
                .Where(u => EF.Functions.ILike(u.Username, query))
                .FirstOrDefaultAsync();

            if (exactMatchUser != null)
            {
                users = new List<User> { exactMatchUser };
            }
            else
            {
                users = await _context.Users
                    .Where(u => EF.Functions.ILike(u.Username, $"%{query}%"))
                    .ToListAsync();
            }

            var userProfileDTOs = new List<UserProfileDTO>();
            foreach (var user in users)
            {
                var FavouriteTeam = await _context.NationalTeams.FirstOrDefaultAsync(team => team.Id == user.NationalTeamId);
                string FavouriteTeamName = "";
                int FavouriteTeamId = 0;
                if (FavouriteTeam != null)
                {
                    FavouriteTeamName = FavouriteTeam.Name;
                    FavouriteTeamId = FavouriteTeam.Id;
                } 

                var team = await _context.Teams.FirstOrDefaultAsync(team => team.Id == user.TeamId);

                userProfileDTOs.Add(new UserProfileDTO
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FavouriteTeam = FavouriteTeamName,
                    FavouriteTeamId = FavouriteTeamId,
                    ProfilePicRef = user.ProfilePicRef,
                    Team = team
                });
            }

            return Ok(userProfileDTOs);
        }

        private string GenerateRefreshToken() {

            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }

    
}

