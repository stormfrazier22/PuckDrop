using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PuckDrop.Application.Connectors;
using PuckDrop.Application.DbContexts;
using PuckDrop.Application.Interfaces;
using PuckDrop.Application.Repositories;
using PuckDrop.Authentication;
using PuckDrop.Core.Objects;
using PuckDrop.Core.Settings;
using PuckDrop.Connectors;
using PuckDrop.Services;

namespace PuckDrop.Startup
{
    public static class DependencyInjectionSetup
    {
        private static IConfiguration _config;

        public static IConfiguration Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

                }
                return _config;
            }
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            });
            services.AddHttpClient<INHLConnector, NHLConnector>("NHLClient",
                client =>
                {
                    client.BaseAddress = new Uri(Config.GetValue<string>("NHL_API_BASE_URL"));
                });
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(Config.GetConnectionString("AppConnection")));


            services.Configure<EmailSettings>(Config.GetSection("EmailSettings"));

            services.AddScoped<ValidateSecretKeyAttribute>();
            services.AddScoped<IAppRepository, AppRepository>();
            services.AddScoped<IGameMonitorService, GameMonitorService>();

            services.AddSingleton(Config);
            services.AddSingleton<INHLConnector, NHLConnector>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IGameScheduleService, GameScheduleService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<INotificationService>(GenerateNotificationService());

            return services;
        }

        private static NotificationService GenerateNotificationService()
        {
            var twilioAccountSid = Config.GetValue<string>("TWILIO_ACCOUNT_ID");
            var twilioAuthToken = Config.GetValue<string>("TWILIO_AUTH_TOKEN");
            var twilioPhoneNumber = Config.GetValue<string>("TWILIO_PHONE_NUMBER");
            var notificationService = new NotificationService(twilioAccountSid, twilioAuthToken, twilioPhoneNumber);
            return notificationService;

        }

    }
}
