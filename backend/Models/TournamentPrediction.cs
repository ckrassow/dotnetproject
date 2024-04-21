namespace EuroPredApi.Models;
public enum TournamentPredictionType {
    TotalGoals,
    TotalYellows,
    TotalReds,
    TotalCorners,
    TotalFirstHalfGoals,
    TotalSecondHalfGoals,
    TotalPenalties,
    TotalOvertime,
    AverageGoalsGroup,
    AverageGoalsBracket
}
public class TournamentPrediction {

    public int Id { get; set;}
    public int UserId {get; set;}
    public User? User { get; set;}
    public TournamentPredictionType PredictionType {get; set;}
    public string? PredictionValue {get; set;}
}