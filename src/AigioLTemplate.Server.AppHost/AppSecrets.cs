namespace AigioLTemplate.Server.AppHost;

static partial class AppHostHelper
{
    internal const string DevDbHost = "";
    internal const string DevDbPort = "";
    internal const string DevDbUsername = "";
    internal const string DevDbPassword = "";
    internal static readonly string DevDbConnectionString = $"Host={DevDbHost};Port={DevDbPort};Username={DevDbUsername};Password={DevDbPassword}";
    internal const string CACHE_HOST = "";
    internal const string CACHE_PORT = "";
    internal const string CACHE_PASSWORD = "";
    internal const string CACHE_URI = $"redis://:${CACHE_PASSWORD}@{CACHE_HOST}:{CACHE_PORT}";
    internal const string ConnectionStrings__cache = $"{CACHE_HOST}:{CACHE_PORT},password={CACHE_PASSWORD}";
    internal const string MESSAGING_HOST = "";
    internal const string MESSAGING_PORT = "";
    internal const string MESSAGING_URI = $"amqp://{{MESSAGING_USERNAME}}:{{MESSAGING_PASSWORD}}@{MESSAGING_HOST}:{MESSAGING_PORT}/";
    internal const string ConnectionStrings__messaging = $"amqp://{{MESSAGING_USERNAME}}:{{MESSAGING_PASSWORD}}@{MESSAGING_HOST}:{MESSAGING_PORT}";
    internal const string MEILISEARCH_HOST = "";
}