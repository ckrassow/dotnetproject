namespace EuroPredApi.Models;
using EuroPredApi.Types;

public class TeamPrediction {

    public int Id { get; set;}
    public TeamPredictionType PredictionType {get; set;}
    public int? NationalTeamId {get; set;}
    public NationalTeam? NationalTeam {get; set;}
}