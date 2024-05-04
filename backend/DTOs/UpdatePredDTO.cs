using EuroPredApi.Models;
using EuroPredApi.Types;

namespace EuroPredApi.DTOs;

public class PlayerPredictionUpdateDTO
{   
    public int PlayerId { get; set; }
}

public class PlayerPredictionResponseDTO 
{
    public Player Player { get; set; }
    public PlayerPredictionType PlayerPredictionType { get; set; }
    public string PredictionType { get; set; }
}

public class NationalTeamPredictionUpdateDTO
{
    public int NationalTeamId { get; set; }
}


public class TournamentPredictionUpdateDTO
{
    public string? PredictionValue { get; set; }
    public int Id { get; set; } 
}
public class UserPredictionPlayerPredictionDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PredictionId { get; set; }
    public PlayerPredictionDTO Prediction { get; set; }
    public string PredictionTypeString { get; set; }
}

public class PlayerPredictionDTO
{
    public int Id { get; set; }
    public PlayerPredictionType PredictionType { get; set; }
    public int? PlayerId { get; set; }
}