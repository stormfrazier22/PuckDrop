using PuckDrop.Model;

namespace PuckDrop.Services
{
    public class MessageService : IMessageService
    {
        private readonly GameHelper _helpers = new GameHelper();
        public MessageService() { }


        public string GenerateGameInfoMessage(GameInfo game)
        {
            if (!_helpers.ValidateGame(game))
            {
                throw new ArgumentException("One or more parameters is blank. Cannot send text message");
            }
            var gameInfo = game.Dates.FirstOrDefault()?.Games.FirstOrDefault();
            var gameType = _helpers.ToDescription(gameInfo.GameType);
            var awayTeam = gameInfo.Teams.Away.Team.Name;
            var homeTeam = gameInfo.Teams.Home.Team.Name;
            var arena = gameInfo.Venue.Name;
            var tzi = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            var centralTime = TimeZoneInfo.ConvertTimeFromUtc(gameInfo.GameDate, tzi).ToString("h:mm tt");

            return ($"The {homeTeam} play a {gameType} game today vs the {awayTeam} at {centralTime} CST at {arena}");
        }

        public string GenerateReminderMessage(GameInfo game)
        {
            //TODO
            return "";
        }

        public string GenerateFinalScoreMessage(GameInfo game)
        {
            //TODO
            return "";
        }
    }
}
