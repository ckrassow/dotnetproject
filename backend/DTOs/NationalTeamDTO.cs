namespace EuroPredApi.DTOs;

public class NationalTeamDTO {

    public int Id { get; set; }
    public string? Name { get; set; }
    public int PlayoffAppearences { get; set; }
    public int FifaRanking { get; set; }
    public string? Group { get; set; }
    public string? ImagePath { get; set; }
}