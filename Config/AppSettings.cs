namespace IminetSite.Configuration
{
    public class AppSettings
    {
        public string? OutputPath { get; set; }
        public string? Host { get; set; }
        public string? SiteTitle { get; set; }

        public GoogleSettings? Google { get; set; }
        public GithubSettings? Github { get; set; }
    }

    public class GoogleSettings
    {
        public string? GtagId { get; set; }
    }

    public class GithubSettings
    {
        public string? Username { get; set; }
        public string? Token { get; set; }
    }
}