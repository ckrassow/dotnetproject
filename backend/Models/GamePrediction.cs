using System.ComponentModel.DataAnnotations;
namespace EuroPredApi.Models;

public class GamePrediction
{
    [Key]
    public int UserId { get; set; }
    [Key]
    public int GameId { get; set; }

    public User User { get; set; }
    public Game Game { get; set; }
    public int? PredictedHomeScore { get; set; }
    public int? PredictedAwayScore { get; set; }
    public DateTime UtcDate { get; set; }
    public Boolean Completed { get; set; }
}