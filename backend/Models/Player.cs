namespace EuroPredApi.Models;

public class Player {

    public int Id { get; set; }
    public int No { get; set; }
    public string? Pos { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public int Caps { get; set; }
    public int Goals { get; set; }
    public string? Club { get; set; }
    public int NationalTeamId { get; set; }
    public NationalTeam NationalTeam { get; set; }
    public string? ImagePath { get; set; }
}