namespace EuroPredApi.Models;

public class Game
{
    public int Id { get; set; }
    public DateTime UtcDate { get; set; }
    public string Status { get; set; }
    public int? Matchday { get; set; }
    public string Stage { get; set; }
    public string? Group { get; set; }
    public DateTime LastUpdated { get; set; }

    public string? HomeTeam { get; set; }
    public string? AwayTeam { get; set; }

    public int CompetitionId { get; set; }
    public Competition Competition { get; set; }

    public Score Score { get; set; }
}

public class Competition
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Emblem { get; set; }
}

public class Score
{
    public int Id { get; set; }
    public string? Winner { get; set; }

    public FullTime FullTime { get; set; }
    public HalfTime HalfTime { get; set; }
}

public class FullTime
{
    public int Id { get; set; }
    public int? Home { get; set; }
    public int? Away { get; set; }
}

public class HalfTime 
{
    public int Id { get; set; }
    public int? Home { get; set; }
    public int? Away { get; set; }
}
