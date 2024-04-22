namespace EuroPredApi.DTOs;
using EuroPredApi.Types;

public class TeamPredictionsDTO
{
    public int Id { get; set; }
    public TeamPredictionType PredictionType { get; set; }
    public int? PlayerId { get; set; }
}