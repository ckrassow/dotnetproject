namespace EuroPredApi.Models;

public enum TeamPredictionType {
    MostGoals,
    LeastConceded,
    MostGoalsGroup,
    MostYellowCards,
    MostPassesPlayed,
    MostFoulsAgainst,
    MostFoulsFor,
    MostShotsTaken,
    MostGoalsBracket,
    Winners
}
public class TeamPrediction {

    public int Id { get; set;}
    public int UserId {get; set;}
    public User? User { get; set;}
    public TeamPredictionType PredictionType {get; set;}
    public int NationalTeamId {get; set;}
    public NationalTeam? NationalTeam {get; set;}
}