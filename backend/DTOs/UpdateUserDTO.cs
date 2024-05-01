using EuroPredApi.DTOs;
using EuroPredApi.Models;

public class UpdateUserDTO
{
    public string? Username { get; set; }
    public string? FavouriteTeam { get; set; }
    public TournamentPredictionUpdateDTO? TournamentPredictions { get; set; }
    public PlayerPredictionUpdateDTO? PlayerPredictions { get; set; }
    public TournamentPredictionUpdateDTO? TeamPredictions { get; set; }
}

public class UpdateUserProfileDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public NationalTeam? FavouriteTeam { get; set; } // Assuming team is represented by an integer ID
}

public class UpdateProfilePictureDTO
{
    public string ProfilePicRef { get; set; }
}
/*
{
    "username": "new_username",
    "favouriteTeam": "new_favourite_team",
    "tournamentPredictions": {
        "predictionType": "Winner",
        "predictionValue": "Team A"
    },
    "playerPredictions": {
        "playerId": 1,
        "predictionType": "Top Scorer"
    },
    "teamPredictions": {
        "nationalTeamId": 1,
        "predictionType": "Finalist"
    }
}
*/