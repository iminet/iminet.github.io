using Iminetsoft.Settings;
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
          services.AddSingleton<INIFile>(new Iminetsoft.Settings.INIFile(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "config", "config.ini"), true, true));
        })
        .RunAsync();
  }
}