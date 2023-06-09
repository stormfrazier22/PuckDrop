using Microsoft.Extensions.Configuration;
using PuckDrop.Application.Connectors;
using PuckDrop.Connectors;
using PuckDrop.Model;
using static PuckDrop.Connectors.NHLConnector;

namespace PuckDrop.Services
{
    public class GameScheduleService : IGameScheduleService
    {
        private readonly INHLConnector _httpConnector;
        private readonly IConfiguration _config;
        public GameScheduleService(IConfiguration config, INHLConnector nhlConnector)
        {
            _httpConnector = nhlConnector;
            _config = config;
        }

        public async Task<HttpResponseMessage> GetTodayGame(int teamId)
        {
            try
            {
                return await _httpConnector.GetTodayGame(teamId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
