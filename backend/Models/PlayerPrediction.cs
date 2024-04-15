namespace EuroPredApi.Models;

public class PlayerPrediction {

    public int Id { get; set;}
    public int UserId {get; set;}
    public User User { get; set;}
    public int PredictionNumber {get; set;}
    public int PlayerId {get; set;}
    public Player Player {get; set;}
}