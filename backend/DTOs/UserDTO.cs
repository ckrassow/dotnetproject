using EuroPredApi.Models;

namespace EuroPredApi.DTOs;

public class UserDTO {

    public int Id { get; set;}
    public string Username { get; set;}
    public string Email {get; set;}
    public ICollection<PlayerPrediction>? PlayerPredictions { get; set;}
    public ICollection<TeamPrediction>? TeamPredictions { get; set;}
    public ICollection<TournamentPrediction>? TournamentPredictions { get; set;}
    public int? TeamId {get; set;}
    public Team? Team {get; set;}
    public string Token {get; set;}


}