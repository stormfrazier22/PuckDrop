using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PuckDrop.Application.Interfaces;
using PuckDrop.Core.Models;
using PuckDrop.Core.Objects;
using PuckDrop.Model;

namespace PuckDrop.Services
{
    public class GameMonitorService : IGameMonitorService
    {
        private readonly IGameScheduleService _gameScheduleService;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        private readonly IMessageService _messageService;
        private readonly IAppRepository _appRepository;

        public GameMonitorService(
            IGameScheduleService gameScheduleService,
            INotificationService notificationService,
            IEmailService emailService,
            IMessageService messageService,
            IAppRepository appRepository)
        {
            _gameScheduleService = gameScheduleService;
            _notificationService = notificationService;
            _emailService = emailService;
            _messageService = messageService;
            _appRepository = appRepository;
        }
        public async Task ProcessUserNotifications()
        {
            var users = await _appRepository.GetUsersAsync();
            var notificationTasks = users.Select(user => NotifyUserGameInfo(user)).ToList();

            await Task.WhenAll(notificationTasks);
        }
        private async Task NotifyUserGameInfo(User user)
        {
            try
            {
                var gameInfo = await GetGameInfo(user.TeamId);

                if (gameInfo != null && gameInfo.TotalGames > 0)
                {
                    await NotifyGameStart(gameInfo, user.PhoneNumber);
                }
            }
            catch (Exception ex)
            {
                await SendErrorNotification(ex);
            }
        }
        private async Task<GameInfo> GetGameInfo(int teamId)
        {
            var response = await _gameScheduleService.GetTodayGame(teamId);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GameInfo>(responseContent);
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }
        private async Task NotifyGameStart(GameInfo game, string phoneNumber)
        {
            var message = _messageService.GenerateGameInfoMessage(game);
            await _notificationService.SendSMSAsync(message, phoneNumber);
        }
        private async Task SendErrorNotification(Exception ex)
        {
            await _emailService.SendErrorEmail(ex.ToString()).ConfigureAwait(false);
        }

        //TODO
        private async Task NotifyGameReminder(GameInfo game)
        {

        }
        //TODO
        private async Task NotifyGameEnd(GameInfo game)
        {

        }
    }
}
