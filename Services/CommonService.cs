using System.Reflection;
using Iminetsoft.Iminetcore.Application;
using Microsoft.Extensions.Logging;

namespace IminetSite.Services
{
    public class CommonService
    {
        private readonly ILogger<GithubService>? logger;
        private readonly IReadOnlySettings? settings;

        public string AppName { get; } = AppInfo.AppName;   
        public string? AppVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        public CommonService(ILogger<GithubService>? logger, IReadOnlySettings? settings)
        {
            this.settings = settings;
            this.logger = logger;
        }

        public string Probe()
        {
            return $"{settings.GetString("SiteUrl")} :: {settings.GetString("GITHUB_USER")}";
        }

        public Dictionary<string,object> SystemCheck()
        {
            var result = new Dictionary<string,object>()
            {
                { "Application", AppName },
                { "Version", AppVersion ?? String.Empty },
                { "Site rendered", @DateTime.Now.ToString("yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture) },
                { "Site URL", settings.GetString("SiteUrl") },
            };

            return result;
        }
    }
}