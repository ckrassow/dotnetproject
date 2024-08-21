
public class LiveGameDTO
{   
    public int Id { get; set; } 
    public string Status { get; set; }
    public DateTime UtcDate { get; set; }
    public int? Matchday { get; set; }
    public string Stage { get; set; }
    public string? Group { get; set; }
    public string? HomeTeam { get; set; }
    public string? AwayTeam { get; set; }
    public string? Winner { get; set; }
    public FullTimeDTO? FullTimeScore { get; set; }
    public HalfTimeDTO? HalfTimeScore { get; set;}
    public DateTime LastUpdated { get; set; }
}

public class FullTimeDTO
{
    public int? Home { get; set; }
    public int? Away { get; set; }
}

public class HalfTimeDTO 
{
    public int? Home { get; set; }
    public int? Away { get; set; }
}