using System.Text.Json;
using EuroPredApi.Models;
using EuroPredApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EuroPredApi.Services
{
    public class BgService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiKeyService _apiKeyService;
        private readonly IServiceProvider _serviceProvider;

        public BgService(HttpClient httpClient, ApiKeyService apiKeyService, IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _apiKeyService = apiKeyService;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateGameStates();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task UpdateGameStates()
        {
            try
            {   
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var apiKey = _apiKeyService.GetApiKey();
                if (!_httpClient.DefaultRequestHeaders.Contains("X-Auth-Token"))
                {
                    _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", apiKey);
                } 
                
                var response = await _httpClient.GetAsync("http://api.football-data.org/v4/competitions/2018/matches");

                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();

                var matchesResponse = JsonSerializer.Deserialize<RootObject>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (matchesResponse?.Matches != null)
                {
                    var filteredMatches = matchesResponse.Matches.Where(m => m.Stage == "GROUP_STAGE").ToList();

                    foreach (var match in filteredMatches)
                    {   
                        var existingMatch = await context.Games.FirstOrDefaultAsync(g => g.Id == match.Id);

                        if (existingMatch == null)
                        {
                            var newGame = new Game
                            {
                                Id = match.Id,
                                UtcDate = match.UtcDate,
                                Status = match.Status,
                                Matchday = match.Matchday,
                                Stage = match.Stage,
                                Group = match.Group,
                                LastUpdated = match.LastUpdated,
                                HomeTeam = match.HomeTeam.Name ?? "Unknown",
                                AwayTeam = match.AwayTeam.Name ?? "Unknown",
                                Competition = new Competition
                                {
                                    Id = match.Comp?.Id ?? 0,
                                    Name = match.Comp?.Name ?? "Unknown",
                                    Emblem = match.Comp?.Emblem ?? "Missing",
                                },
                                Score = new Score
                                {
                                    Winner = match.Score?.Winner ?? "Unknown",
                                    FullTime = new FullTime
                                    {
                                        Home = match.Score?.FullTime.Home ?? -1,
                                        Away = match.Score?.FullTime?.Away ?? -1,
                                    },
                                    HalfTime = new HalfTime
                                    {
                                        Home = match.Score?.HalfTime?.Home ?? -1,
                                        Away = match.Score?.HalfTime?.Away ?? -1,
                                    }
                                }
                            };

                            await context.Games.AddAsync(newGame);
                        }
                        else
                        {   
                            if (match.Status != null) 
                            {
                                existingMatch.Status = match.Status;
                            }

                            existingMatch.LastUpdated = match.LastUpdated;

                            if (existingMatch.Score != null && match.Score?.Winner != null)
                            {   
                                existingMatch.Score.Winner = match.Score.Winner;
                            }                 

                            if (existingMatch.Score?.FullTime != null && match.Score?.FullTime?.Away != null)
                            {
                                existingMatch.Score.FullTime.Away = match.Score.FullTime.Away;
                            }

                            if (existingMatch.Score?.FullTime != null && match.Score?.FullTime?.Home != null)
                            {
                                   existingMatch.Score.FullTime.Home = match.Score.FullTime.Home;
                            }

                            if (existingMatch.Score?.HalfTime?.Away != null && match.Score?.HalfTime?.Away != null)
                            {
                                existingMatch.Score.HalfTime.Away = match.Score.HalfTime.Away;
                            }

                            if (existingMatch.Score?.HalfTime?.Home != null && match.Score?.HalfTime?.Home != null)
                            {
                                existingMatch.Score.HalfTime.Home = match.Score.HalfTime.Home;
                            }

                        }
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching live games: {ex.Message}");
            }
        }
    }

    public class RootObject
    {
        public Filters Filter { get; set; }
        public ResultSets ResultSet { get; set; }
        public Comps Comp { get; set; }
        public List<Match> Matches { get; set; }

        public class Filters
        {
            public string Season { get; set; }
        }

        public class ResultSets
        {
            public int Count { get; set; }
            public string First { get; set; }
            public string Last { get; set; }
            public int Played { get; set; }
        }

        public class Comps
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Type { get; set; }
            public string Emblem { get; set; }
        }

        public class Match
        {
            public Area Area { get; set; }
            public Comps Comp { get; set; }
            public Season Season { get; set; }
            public int Id { get; set; }
            public DateTime UtcDate { get; set; }
            public string Status { get; set; }
            public int? Matchday { get; set; } 
            public string Stage { get; set; }
            public string? Group { get; set; } 
            public DateTime LastUpdated { get; set; }
            public Team HomeTeam { get; set; }
            public Team AwayTeam { get; set; }
            public Score Score { get; set; }
            public Odds Odds { get; set; }
            public List<Referee> Referees { get; set; }
        }

        public class Area
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Flag { get; set; }
        }

        public class Season
        {
            public int Id { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public int CurrentMatchday { get; set; }
            public object Winner { get; set; }
        }

        public class Team
        {
            public int? Id { get; set; } 
            public string Name { get; set; } 
            public string ShortName { get; set; } 
            public string Tla { get; set; } 
            public string Crest { get; set; } 
        }

        public class Score
        {
            public string? Winner { get; set; }
            public string Duration { get; set; }
            public FullTime FullTime { get; set; }
            public HalfTime HalfTime { get; set; }
        }

        public class FullTime
        {
            public int? Home { get; set; }
            public int? Away { get; set; }
        }

        public class HalfTime
        {
            public int? Home { get; set; }
            public int? Away { get; set; }
        }

        public class Odds
        {
            public string Msg { get; set; }
        }

        public class Referee
        {
            
        }

        public static RootObject FromJson(string jsonString)
        {
            return JsonSerializer.Deserialize<RootObject>(jsonString);
        }
    }

}