using IminetSite.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IminetSite.Services
{
    public class AzureApiService
    {
        private readonly ILogger<AzureApiService>? logger;
        private readonly IConfiguration? configuration;
        private readonly IReadOnlySettings? settings;
        private readonly AppSettings? appsettings;

        public AzureApiService(ILogger<AzureApiService>? logger, IConfiguration? configuration, IReadOnlySettings? settings)
        {
            this.settings = settings;
            this.logger = logger;
            this.configuration = configuration;
            logger?.LogDebug("Azure External API service initialized");

            appsettings = configuration?.Get<AppSettings>();
        }
    }
}