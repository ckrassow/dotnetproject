namespace EuroPredApi.Models;
using EuroPredApi.Types;

public class PlayerPrediction {

    public int Id { get; set;}
    public PlayerPredictionType PredictionType {get; set;}
    public int? PlayerId {get; set;}
    public Player? Player {get; set;}
}