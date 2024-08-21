using EuroPredApi.Utils;

namespace EuroPredApi.Models;

public class PredictionTeam {

    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Member> Members { get; set; }
    public ICollection<TeamPrediction<PlayerPrediction>> TeamPlayerPredictions { get; set; }
    public ICollection<TeamPrediction<NationalTeamPrediction>> TeamNationalTeamPredictions { get; set; }
    public ICollection<TeamPrediction<TournamentPrediction>> TeamTournamentPredictions { get; set; }
}

public class Member
{
    public int Id { get; set; }
    public int TeamMemberId { get; set; }
    public User TeamMember { get; set; }
    public int TeamId { get; set; }
    public PredictionTeam Team { get; set; }
    public bool IsCaptain { get; set; }
}

public class TeamPrediction<T>
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public PredictionTeam Team { get; set; }

    public int PredictionId { get; set; }
    public T Prediction { get; set; }
    public string PredictionTypeString { get; set; }

}