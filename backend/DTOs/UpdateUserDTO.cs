using EuroPredApi.DTOs;

public class UpdateUserDTO
{
    public string? Username { get; set; }
    public string? FavouriteTeam { get; set; }
    public TournamentPredictionUpdateDTO? TournamentPredictions { get; set; }
    public PlayerPredictionUpdateDTO? PlayerPredictions { get; set; }
    public TournamentPredictionUpdateDTO? TeamPredictions { get; set; }
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