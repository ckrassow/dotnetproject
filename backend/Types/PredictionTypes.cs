namespace EuroPredApi.Types;
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