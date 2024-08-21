using EuroPredApi.Models;

namespace EuroPredApi.DTOs;

public class UserProfileDTO {

    public required string Username { get; set;}
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public string? ProfilePicRef {get; set;}
    public string? FavouriteTeam {get; set;}
    public int? FavouriteTeamId {get; set;}
    public PredictionTeam? Team {get; set;}
    public int? Points { get; set; }

}