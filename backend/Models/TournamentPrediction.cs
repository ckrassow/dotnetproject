namespace EuroPredApi.Models;

public class TournamentPrediction {

    public int Id { get; set;}
    public int UserId {get; set;}
    public User? User { get; set;}
    public int PredictionNumber {get; set;}
    public string? Prediction {get; set;}
    public string? PredictionType {get; set;}
}