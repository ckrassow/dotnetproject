using EuroPredApi.Types;
using EuroPredApi.Models;

namespace EuroPredApi.Utils;
public class PlayerPrediction
{
    public int Id { get; set; }
    public PlayerPredictionType PredictionType { get; set; }
    public int? PlayerId { get; set; }
    public Player? Player { get; set; }
    public string PredictionTypeString => PredictionType.ToString();
}

public class NationalTeamPrediction
{
    public int Id { get; set; }
    public TeamPredictionType PredictionType { get; set; }
    public int? NationalTeamId { get; set; }
    public NationalTeam? NationalTeam { get; set; }
    public string PredictionTypeString => PredictionType.ToString();
}

public class TournamentPrediction
{
    public int Id { get; set; }
    public TournamentPredictionType PredictionType { get; set; }
    public string? PredictionValue { get; set; }
    public string PredictionTypeString => PredictionType.ToString();
}