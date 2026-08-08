namespace AigioLTemplate.Server.Constants;

public static partial class IfModifiedSinceConstants
{
    public const string KeyIfModifiedSince = "If-Modified-Since";
    public static readonly DateTimeOffset timestampMinValue = new(2025, 7, 27, 0, 0, 0, TimeSpan.FromHours(8));

    public static DateTimeOffset? GetTimestamp(long? timestamp)
    {
        if (timestamp.HasValue)
        {
            var timestamp2 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp.Value);
            if (timestamp2 > timestampMinValue)
            {
                return timestamp2;
            }
        }
        return null;
    }
}