using EuroPredApi.Utils;

namespace EuroPredApi.Models;

public class User {
    public int Id { get; set;}
    public string Username { get; set;}
    public string PasswordHash { get; set;}
    public string? FirstName { get; set;}
    public string? LastName { get; set;}
    public string? ProfilePicRef { get; set;}
    public int Points { get; set; } = 0;
    public int? NationalTeamId { get; set;}
    public NationalTeam? FavouriteTeam { get; set;}
    public string? RefreshToken { get; set; } 
    public DateTime RefreshTokenExpiryTime { get; set; }
    public ICollection<UserPrediction<PlayerPrediction>> UserPlayerPredictions { get; set;}
    public ICollection<UserPrediction<NationalTeamPrediction>> UserNationalTeamPredictions { get; set;}
    public ICollection<UserPrediction<TournamentPrediction>> UserTournamentPredictions { get; set;} 
    public ICollection<Comment> CommentsWritten { get; set; }
    public ICollection<Comment> CommentsReceived { get; set; }
    public ICollection<TeamInvite> TeamInvites { get; set; } 
}

public class UserPrediction<T>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public int PredictionId { get; set; }
    public T Prediction { get; set; }
    public string PredictionTypeString { get; set; }

}

public class Comment {
    public int Id { get; set; } 
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; }
    public int RecipientId { get; set; }
    public User User { get; set; }
}

public class TeamInvite {
    public int Id { get; set;}
    public int SenderId { get; set; } 
    public PredictionTeam Sender {get; set; }
    public int RecipientId { get; set; }
    public User Recipient { get; set; }
    public Boolean Accepted { get; set; }
}