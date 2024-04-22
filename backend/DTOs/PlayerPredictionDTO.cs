using EuroPredApi.Types;

public class PlayerPredictionDTO
{
    public int Id { get; set; }
    public PlayerPredictionType PredictionType { get; set; }
    public int? PlayerId { get; set; }
}