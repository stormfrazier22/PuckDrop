using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System;

[assembly: FunctionsStartup(typeof(PuckDrop.Startup))]

namespace PuckDrop
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient("PuckDropClient", client =>
            {
                client.BaseAddress = new Uri(Config["NHLBaseUrl"]);
                client.DefaultRequestHeaders.Add("Secret-Key", Config["PuckDropSecretKey"]);
            });
        }
    }
}
