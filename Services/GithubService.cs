using IminetSite.Configuration;
using Iminetsoft.Iminetcore.Application;
using Iminetsoft.Iminetcore.AspNet.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework.Internal;
using Octokit;

namespace IminetSite.Services
{
    public class GithubService
    {
        private readonly ILogger<GithubService>? logger;
        private readonly IConfiguration? configuration;
        private readonly AppSettings? appsettings;


        public GithubService(ILogger<GithubService>? logger, IConfiguration? configuration)
        {
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

        public async Task<List<GthubItem>> Public()
        {
            var resp = new List<GthubItem>();
            var github = new GitHubClient(new ProductHeaderValue(AppInfo.AppName));
            var repolist = await github.Repository.GetAllForUser("iminet"); 

            repolist.ToList().ForEach(r => resp.Add(new GthubItem()
            {
                Name = r.Name,
                CreatedAt = r.CreatedAt.DateTime,
                Description = r.Description,
                FullName = r.FullName,
                PushedAt = r.PushedAt?.DateTime,
                UpdatedAt = r.UpdatedAt.DateTime,
                Url = new Uri(r.Url)
            }));

            return resp;
        }
    }
}