using PuckDrop.Model;
namespace PuckDrop.Services
{
    public interface IGameScheduleService
    {
        Task<HttpResponseMessage> GetTodayGame(int teamId);
    }
}
