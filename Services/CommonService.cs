using Microsoft.Extensions.Logging;

namespace IminetSite.Services
{
    public class CommonService
    {
        private readonly ILogger<GithubService>? logger;
        private readonly IReadOnlySettings? settings;

        public CommonService(ILogger<GithubService>? logger, IReadOnlySettings? settings)
        {
            this.settings = settings;
            this.logger = logger;
        }

        public string Probe()
        {
            return $"{settings.GetString("SiteUrl")} :: {settings.GetString("GITHUB_USER")}";
        }
    }
}