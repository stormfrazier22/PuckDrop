using PuckDrop.Model;

namespace PuckDrop.Services
{
    public interface IMessageService
    {
        string GenerateGameInfoMessage(GameInfo game);
        string GenerateReminderMessage(GameInfo game);
        string GenerateFinalScoreMessage(GameInfo game);
    }
}
