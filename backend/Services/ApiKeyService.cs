
namespace EuroPredApi.Services
{
    public class ApiKeyService
    {
        private readonly IConfiguration _configuration;

        public ApiKeyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetApiKey()
        {
            return _configuration.GetValue<string>("GAME_DATA_API_KEY");
        }
    }
}