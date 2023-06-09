  using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using PuckDrop.Model;
using PuckDrop.Services;

namespace ReminderFunctions
{
    public class GameScheduleFunction
    {
        private readonly HttpClient _client;

        public GameScheduleFunction(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("PuckDropClient");
        }

        [FunctionName("DailyReminderFunction")]
        public async Task Run([TimerTrigger("0 0 12 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            HttpResponseMessage response;

            try
            {
                response = await _client.PostAsync("",null);
            }
            catch (Exception ex)
            {
                log.LogInformation($"Exception caught: {ex.Message}");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                var game = await response.Content.ReadAsAsync<GameInfo>();

                //TODO
            }
            else
            {
                log.LogInformation($"Failed to call the API. StatusCode: {response.StatusCode}");
            }
        }
    }

}
