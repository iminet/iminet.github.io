namespace IminetSite.Models
{
    public class FortniteSeason
    {
        public string Title { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }

        public bool IsInProgress =>
            DateOnly.FromDateTime(DateTime.Today) >= StartDate &&
            DateOnly.FromDateTime(DateTime.Today) <= EndDate;

        public string StatusText => IsInProgress
            ? "⏳ In progress ..."
            : "✅ Done!";
    }
}