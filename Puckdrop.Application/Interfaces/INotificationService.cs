namespace PuckDrop.Services
{
    public interface INotificationService
    {
        Task SendSMSAsync(string message, string phoneNumber);
    }
}
