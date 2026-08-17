using IminetSite.Configuration;
using Iminetsoft.Iminetcore.Application;
using Iminetsoft.Iminetcore.AspNet.Models;
using Microsoft.Azure.Search.Common;
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
        private string? github_token;


        public GithubService(ILogger<GithubService>? logger, IConfiguration? configuration, IReadOnlySettings? settings)
        {
            this.settings = settings;
            this.logger = logger;
            this.configuration = configuration;
            logger?.LogDebug("Github service initialized");

            appsettings = configuration?.Get<AppSettings>();

            this.github_token = settings.GetString("GITHUB_TOKEN");
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
            /* This needs in the future for accessing authenticated */
            var token = settings.GetString("GITHUB_TOKEN");
            if (String.IsNullOrWhiteSpace(token)) throw new Exception("Missing GitHub token");

            var resp = new List<GithubItem>();
            var github = new GitHubClient(new ProductHeaderValue(AppInfo.AppName));
            github.Credentials = new Credentials(token);

            // Public access, public repo
            var repolist = await github.Repository.GetAllForCurrent();

            repolist.Where(r => !r.Private && !r.Archived).ToList().ForEach(r => resp.Add(new GithubItem()
            {
                Name = r.Name,
                CreatedAt = r.CreatedAt.DateTime,
                Description = r.Description,
                FullName = r.FullName,
                PushedAt = r.PushedAt?.DateTime,
                UpdatedAt = r.UpdatedAt.DateTime,
                Url = new Uri(r.HtmlUrl),
                IsFork = r.Fork
            }));

            return resp;
        }
    }
}