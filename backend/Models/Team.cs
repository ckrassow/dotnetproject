namespace EuroPredApi.Models;

public class Team {

    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<User> Members { get; set; }
    public ICollection<PlayerPrediction> PlayerPredictions { get; set; }
    public ICollection<TeamPrediction> TeamPredictions { get; set; }
    public ICollection<TournamentPrediction> TournamentPredictions { get; set; }

}