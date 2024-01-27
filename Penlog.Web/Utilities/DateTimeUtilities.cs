
namespace Penlog.Utilities
{
    public static class DateTimeUtilities
    {
        public static string Span(DateTimeOffset dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds >= 30240000 * 2)
                return (timeSpan.Days / 365) + " years ago";
            if (timeSpan.TotalSeconds >= 30240000)
                return (timeSpan.Days / 365) + " year ago";

            if (timeSpan.TotalSeconds >= 2520000 * 2)
                return (timeSpan.Days / 30) + " months ago";
            if (timeSpan.TotalSeconds >= 2520000)
                return (timeSpan.Days / 30) + " month ago";

            if (timeSpan.TotalSeconds >= 604800 * 2)
                return (timeSpan.Days / 7) + " weeks ago";
            if (timeSpan.TotalSeconds >= 604800)
                return (timeSpan.Days / 7) + " week ago";

            if (timeSpan.TotalSeconds >= 86400 * 2)
                return (timeSpan.Days) + " days ago";
            if (timeSpan.TotalSeconds >= 86400)
                return (timeSpan.Days) + " day ago";

            if (timeSpan.TotalSeconds >= 3600 * 2)
                return (timeSpan.Hours) + " hours ago";
            if (timeSpan.TotalSeconds >= 3600)
                return (timeSpan.Hours) + " hour ago";

            if (timeSpan.TotalSeconds >= 60 * 2)
                return (timeSpan.Minutes) + " minutes ago";
            if (timeSpan.TotalSeconds >= 60)
                return (timeSpan.Minutes) + " minute ago";

            if (timeSpan.TotalSeconds >= 1 * 2)
                return (timeSpan.Seconds) + " seconds ago";
            if (timeSpan.TotalSeconds >= 1)
                return (timeSpan.Seconds) + " second ago";

            return null;
        }
    }
}
