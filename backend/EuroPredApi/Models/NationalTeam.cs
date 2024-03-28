
namespace EuroPredApi.Models;

public class NationalTeam {

    public int Id { get; set; }
    public string? Name { get; set; }
    public int PlayoffAppearences { get; set; }
    public int FifaRanking { get; set; }
    public string? Group { get; set; }
    public ICollection<Player> Players { get; set; }
}