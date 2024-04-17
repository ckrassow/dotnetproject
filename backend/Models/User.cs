namespace EuroPredApi.Models;

public class User {
    public int Id { get; set;}
    public string Username { get; set;}
    public string PasswordHash { get; set;}
    public int? NationalTeamId { get; set;}
    public NationalTeam? FavouriteTeam { get; set;}
    public ICollection<PlayerPrediction>? PlayerPredictions { get; set;}
    public ICollection<TeamPrediction>? TeamPredictions { get; set;}
    public ICollection<TournamentPrediction>? TournamentPredictions { get; set;}
    public int? TeamId {get; set;}
    public Team? Team {get; set;}
    public string? RefreshToken {get; set;} 

}