namespace EuroPredApi.Models;
using EuroPredApi.Types;

public class TournamentPrediction {

    public int Id { get; set;}
    public TournamentPredictionType PredictionType {get; set;}
    public string? PredictionValue {get; set;}
}