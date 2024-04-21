namespace EuroPredApi.Models;

public enum PlayerPredictionType {
    TopScorer,
    MostAssists,
    MostCleanSheets,
    MostYellowCards,
    MostPassesPlayed,
    MostFoulsAgainst,
    MostFoulsFor,
    MostShotsTaken,
    MostHeadersWon,
    BestPlayerOfTournament
}
public class PlayerPrediction {

    public int Id { get; set;}
    public int UserId {get; set;}
    public User User { get; set;}
    public PlayerPredictionType PredictionType {get; set;}
    public int PlayerId {get; set;}
    public Player Player {get; set;}
}