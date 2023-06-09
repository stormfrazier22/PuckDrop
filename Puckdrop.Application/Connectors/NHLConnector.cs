using Newtonsoft.Json;
using PuckDrop.Application.Connectors;
using PuckDrop.Model;
using System.Net;

namespace PuckDrop.Connectors
{
    public class NHLConnector : INHLConnector
    {

        private readonly HttpClient _client;

        public NHLConnector(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("NHLClient");
        }
        public async Task<HttpResponseMessage> GetTodayGame(int teamId)
        {
            var today = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
            var yesterday = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
            return await _client.GetAsync($"schedule?teamId={teamId}&startDate={yesterday}&endDate={today}");
        }




    }
}
