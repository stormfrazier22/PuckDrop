namespace PuckDrop.Services
{
    public interface IEmailService
    {
        Task SendErrorEmail(string errorDetails);
    }
}
