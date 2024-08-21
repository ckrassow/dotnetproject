using EuroPredApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EuroPredApi.Services
{
    public class DailyJob : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DailyJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await DoDailyTask(); 
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); 
            }
        }

        private async Task DoDailyTask()
        {

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var gamePredictions = await context.GamePredictions.ToListAsync();
                var now = DateTime.UtcNow;

                foreach (var prediction in gamePredictions)
                {   

                    if (now.CompareTo(prediction.UtcDate) > 0)
                    {
                        if (!prediction.Completed)
                        {   
                            var user = await context.Users.FindAsync(prediction.UserId);

                            if (user == null)
                            {   
                                Console.WriteLine("User could not be found");
                                continue;
                            }

                            var game = await context.Games.FindAsync(prediction.GameId);
                            if (game != null)
                            {
                                if (game.Score.Winner != null)
                                {   
                                    int? correctHomeScore = game.Score.FullTime.Home;
                                    int? correctAwayScore = game.Score.FullTime.Away;
                                    if (prediction.PredictedHomeScore == correctHomeScore && prediction.PredictedAwayScore == correctAwayScore)
                                    {   
                                        user.Points += 10;
                                    }
                                    else if (correctHomeScore > correctAwayScore && prediction.PredictedHomeScore > prediction.PredictedAwayScore)
                                    {
                                        user.Points += 5;
                                    }
                                    else if (correctHomeScore < correctAwayScore && prediction.PredictedHomeScore < prediction.PredictedAwayScore)
                                    {
                                        user.Points += 5;
                                    }
                                    else if (correctHomeScore == correctAwayScore && prediction.PredictedHomeScore == prediction.PredictedAwayScore)
                                    {
                                        user.Points += 5;
                                    }
                                    else
                                    {
                                        user.Points += 0;
                                    }

                                    prediction.Completed = true; 
                                }
                            }
                            
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DailyJob: {ex.Message}");
            }
        }
    }
}