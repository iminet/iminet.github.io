using System.Net.Http.Json;
using System.Reflection;
using Iminetsoft.Iminetcore.Application;
using Iminetsoft.Iminetcore.AspNet.Models;
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

        public async Task<string> Probe()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetFromJsonAsync<ApiResult<string>>($"{settings.GetString("BACKEND_API_URL")}api/v2/ping");

                return result?.Data ?? "Error";
            }
        }

        public Dictionary<string,object> SystemCheck()
        {
            var result = new Dictionary<string,object>()
            {
                { "Application", AppName },
                { "Version", AppVersion ?? String.Empty },
                { "Site rendered", @DateTime.Now.ToString("yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture) },
                { "Site URL", settings.GetString("SiteUrl") },
                { "Config test string", settings.GetString("STATIQ_TEST") }
            };

            return result;
        }
    }
}