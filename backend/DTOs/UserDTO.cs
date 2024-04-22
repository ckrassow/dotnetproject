using EuroPredApi.Models;

namespace EuroPredApi.DTOs;

public class UserDTO {

    public int Id { get; set;}
    public string Username { get; set;}
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public NationalTeam? FavouriteTeam { get; set;}
    public ICollection<PlayerPrediction> PlayerPredictions { get; set;}
    public ICollection<TeamPrediction> TeamPredictions { get; set;}
    public ICollection<TournamentPrediction> TournamentPredictions { get; set;}
    public int? TeamId {get; set;}
    public Team? Team {get; set;}

}