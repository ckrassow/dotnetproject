namespace EuroPredApi.Models;
using EuroPredApi.Types;

public class User {
    public int Id { get; set;}
    public string Username { get; set;}
    public string PasswordHash { get; set;}
    public string? FirstName { get; set;}
    public string? LastName { get; set;}
    public string? ProfilePicRef {get; set;}
    public int? NationalTeamId { get; set;}
    public NationalTeam? FavouriteTeam { get; set;}
    public int? TeamId { get; set; }
    public string? RefreshToken { get; set; } 
    public DateTime RefreshTokenExpiryTime { get; set; }
    public ICollection<UserPrediction<PlayerPrediction>> UserPlayerPredictions { get; set;}
    public ICollection<UserPrediction<TeamPrediction>> UserTeamPredictions { get; set;}
    public ICollection<UserPrediction<TournamentPrediction>> UserTournamentPredictions { get; set;}  

}

public class PlayerPrediction
{
    public int Id { get; set; }
    public PlayerPredictionType PredictionType { get; set; }
    public int? PlayerId { get; set; }
    public Player? Player { get; set; }
    public string PredictionTypeString => PredictionType.ToString();
}

public class TeamPrediction
{
    public int Id { get; set; }
    public TeamPredictionType PredictionType { get; set; }
    public int? NationalTeamId { get; set; }
    public NationalTeam? NationalTeam { get; set; }
    public string PredictionTypeString => PredictionType.ToString();
}

public class TournamentPrediction
{
    public int Id { get; set; }
    public TournamentPredictionType PredictionType { get; set; }
    public string? PredictionValue { get; set; }
    public string PredictionTypeString => PredictionType.ToString();
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