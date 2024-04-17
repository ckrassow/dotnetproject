namespace EuroPredApi.Models;

public class TeamPrediction {

    public int Id { get; set;}
    public int UserId {get; set;}
    public User? User { get; set;}
    public int PredictionNumber {get; set;}
    public int NationalTeamId {get; set;}
    public NationalTeam? NationalTeam {get; set;}
}