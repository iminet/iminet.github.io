using IminetSite.Configuration;
using Iminetsoft.Iminetcore.Application;
using Iminetsoft.Iminetcore.AspNet.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Octokit;

namespace IminetSite.Services
{
    public class GithubService
    {
        private readonly ILogger<GithubService>? logger;
        private readonly IConfiguration? configuration;
        private readonly IReadOnlySettings? settings;
        private readonly AppSettings? appsettings;


        public GithubService(ILogger<GithubService>? logger, IConfiguration? configuration, IReadOnlySettings? settings)
        {
            this.settings = settings;
            this.logger = logger;
            this.configuration = configuration;
            logger?.LogDebug("Github service initialized");

            appsettings = configuration?.Get<AppSettings>();
        }

        public class GithubSettings
        {
            public string? UserName { get; set; }
            public string? Token { get; set; }
        }

        public string Test()
        {
            return settings.GetString("STATIQ_TEST");
        }

        public async Task<List<GithubItem>> PublicRepos()
        {
            //var token = settings.GetString("GITHUB_TOKEN");
            //if (String.IsNullOrWhiteSpace(token)) return new();

            var resp = new List<GithubItem>();
            var github = new GitHubClient(new ProductHeaderValue(AppInfo.AppName));
            var repolist = await github.Repository.GetAllForUser("iminet"); 

            repolist.Where(r => !r.Private).ToList().ForEach(r => resp.Add(new GithubItem()
            {
                Name = r.Name,
                CreatedAt = r.CreatedAt.DateTime,
                Description = r.Description,
                FullName = r.FullName,
                PushedAt = r.PushedAt?.DateTime,
                UpdatedAt = r.UpdatedAt.DateTime,
                Url = new Uri(r.Url),
            }));

            return resp;
        }
    }
}