using IminetSite.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Statiq.App;
using Statiq.Web;

namespace IminetSite
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateWeb(args)
        .ConfigureServices(services =>
        {
          services.AddSingleton<GithubService>();
        })
        .RunAsync();
  }
}