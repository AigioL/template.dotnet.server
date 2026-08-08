namespace AigioLTemplate.Server.AppHost;

static partial class AppHostHelper
{
    internal const string repoName = "AigioLTemplate.Server";
    internal const int postgres_port = 30001;
    internal const int redis_port = 30002;
    internal const int meilisearch_port = 30003;

#if USE_LOCAL_DB
    internal static IResourceBuilder<PostgresDatabaseResource> db_aigioltemplate = null!;
    internal static IResourceBuilder<PostgresDatabaseResource> db_aigioltemplate_apig = null!;
#else
    internal static IResourceBuilder<ParameterResource> db_aigioltemplate = null!;
    internal static IResourceBuilder<ParameterResource> db_aigioltemplate_apig = null!;
#endif

    internal static IResourceBuilder<MeilisearchResource> meilisearch = null!;
    internal static IResourceBuilder<ParameterResource> meilisearch_p = null!;

    internal const string ConnectionStringEnvironmentName = "ConnectionStrings__";

    const string imageRegistryUrl = "";

    internal static string? GetPostgreSQLDatabasePath()
    {
        if (OperatingSystem.IsWindows())
        {
            var projPath = ProjPath;
            if (string.IsNullOrWhiteSpace(projPath))
            {
                return $@"C:\PostgreSQL\{repoName}";
            }
            // 此路径已在 .gitignore 中忽略
            return Path.Combine(projPath, "res", "postgresql", "data");
        }
        else
        {
            return null;
        }
    }

    internal static string? GetRabbitMQDataPath()
    {
        if (OperatingSystem.IsWindows())
        {
            var projPath = ProjPath;
            if (string.IsNullOrWhiteSpace(projPath))
            {
                return $@"C:\RabbitMQ\{repoName}";
            }
            // 此路径已在 .gitignore 中忽略
            return Path.Combine(projPath, "res", "rabbitmq", "data");
        }
        else
        {
            return @"/RabbitMQ/Data";
        }
    }

    internal static string GetMeilisearchPath()
    {
        if (OperatingSystem.IsWindows())
        {
            var projPath = ProjPath;
            if (string.IsNullOrWhiteSpace(projPath))
            {
                return $@"C:\Meilisearch\{repoName}";
            }
            // 此路径已在 .gitignore 中忽略
            return Path.Combine(projPath, "res", "meilisearch", "data");
        }
        else
        {
            throw new PlatformNotSupportedException(); // 尚未适配其他操作系统
        }
    }

    internal static void WithDataBindMount(IResourceBuilder<PostgresServerResource> builder)
    {
        var databasePath = GetPostgreSQLDatabasePath();
        if (databasePath != null)
        {
            builder.WithDataBindMount(source: databasePath, isReadOnly: false);
        }
    }

    internal static void WithDataBindMount(IResourceBuilder<RabbitMQServerResource> builder)
    {
        // https://aspire.dev/integrations/messaging/rabbitmq/#add-rabbitmq-server-resource-with-data-bind-mount
        var databasePath = GetRabbitMQDataPath();
        if (databasePath != null)
        {
            builder.WithDataBindMount(source: databasePath, isReadOnly: false);
        }
    }

    internal static IResourceBuilder<TDestination> WithPostgresDatabase<TDestination>(
        this IResourceBuilder<TDestination> builder,
#if USE_LOCAL_DB
        IResourceBuilder<PostgresDatabaseResource> db
#else
        IResourceBuilder<ParameterResource> db
#endif
        )
        where TDestination : IResourceWithEnvironment, IResourceWithWaitSupport
    {
#if USE_LOCAL_DB
        builder.WithReference(db);
        builder.WaitFor(db);
#else
        builder.WithEnvironment($"{ConnectionStringEnvironmentName}{db.Resource.Name}", db);
#endif
        return builder;
    }

    public static IResourceBuilder<IResource> AddRedis2(
        IDistributedApplicationBuilder builder,
        [ResourceName] string name,
        int? port)
    {
        //#if USE_LOCAL_REDIS_DB
        // 使用本地 Redis 缓存服务
        //var cache = builder.AddRedis(name, port) // https://github.com/microsoft/aspire/issues/16640
        var cache = builder.AddRedis(name)
            .WithImage("base/redis", "latest");
        if (!string.IsNullOrWhiteSpace(imageRegistryUrl))
        {
            cache = cache
                .WithImageRegistry(imageRegistryUrl);
        }
        return cache;
        //#else
        //        // 使用常量添加参数，连接远程 Redis 缓存服务
        //        var nameUpper = name.ToUpperInvariant();
        //        Dictionary<string, string> env = new()
        //                {
        //                    { $"{nameUpper}_HOST", CACHE_HOST },
        //                    { $"{nameUpper}_PORT", CACHE_PORT },
        //                    { $"{nameUpper}_PASSWORD", CACHE_PASSWORD },
        //                    { $"{nameUpper}_URI", CACHE_URI },
        //                    { $"ConnectionStrings__{name}", ConnectionStrings__cache },
        //                };
        //        var cache = builder.AddParameter(name, ConnectionStrings__cache, secret: true);
        //        return new EnvDictResourceBuilder(cache, env);
        //#endif
    }

    /// <summary>
    /// WithReference + WaitFor + EnvDict
    /// </summary>
    public static IResourceBuilder<TDestination> WithReferenceAndWaitFor<TDestination>(
        this IResourceBuilder<TDestination> builder,
        IResourceBuilder<IResource> source)
        where TDestination : IResourceWithEnvironment, IResourceWithWaitSupport
    {
        if (source is EnvDictResourceBuilder envDict)
        {
            foreach (var kvp in envDict.Environment)
            {
                builder = builder.WithEnvironment(kvp.Key, kvp.Value);
            }
        }
        if (source is IResourceBuilder<IResourceWithConnectionString> source2)
        {
            builder = builder.WithReference(source2);
        }
        return builder.WaitFor(source);
    }

    public static async ValueTask<IResourceBuilder<IResource>> AddRabbitMQ(
        IDistributedApplicationBuilder builder,
        [ResourceName] string name)
    {
        var rabbitmq_username = builder.AddParameter("rabbitmq-username", secret: true);
        var rabbitmq_password = builder.AddParameter("rabbitmq-password", secret: true);
        //#if USE_LOCAL_RABBITMQ
        var rabbitmq = builder.AddRabbitMQ("messaging", rabbitmq_username, rabbitmq_password)
            .WithImageRegistry(imageRegistryUrl)
            .WithImage("base/rabbitmq", "4.2-management")
            //.WithManagementPlugin()
            ;
        WithDataBindMount(rabbitmq);
        return rabbitmq;
        //#else
        //        // 使用常量添加参数，连接远程 RabbitMQ 服务
        //        var rabbitmq_username_value = await rabbitmq_username.Resource.GetValueAsync(CancellationToken.None);
        //        ArgumentNullException.ThrowIfNull(rabbitmq_username_value);
        //        var rabbitmq_password_value = await rabbitmq_password.Resource.GetValueAsync(CancellationToken.None);
        //        ArgumentNullException.ThrowIfNull(rabbitmq_password_value);

        //        var nameUpper = name.ToUpperInvariant();
        //        var connectionStrings = ConnectionStrings__messaging.Replace($"{{{nameUpper}_USERNAME}}", rabbitmq_username_value).Replace($"{{{nameUpper}_PASSWORD}}", rabbitmq_password_value);
        //        Dictionary<string, string> env = new()
        //                {
        //                    { $"{nameUpper}_HOST", MESSAGING_HOST },
        //                    { $"{nameUpper}_PORT", MESSAGING_PORT },
        //                    { $"{nameUpper}_USERNAME", rabbitmq_username_value },
        //                    { $"{nameUpper}_PASSWORD", rabbitmq_password_value },
        //                    { $"{nameUpper}_URI", MESSAGING_URI.Replace($"{{{nameUpper}_USERNAME}}", rabbitmq_username_value).Replace($"{{{nameUpper}_PASSWORD}}", rabbitmq_password_value) },
        //                    { $"ConnectionStrings__{name}", connectionStrings },
        //                };
        //        var rabbitmq = builder.AddParameter(name, ConnectionStrings__cache, secret: true);
        //        return new EnvDictResourceBuilder(rabbitmq, env);
        //#endif
    }

    public static async ValueTask<IResourceBuilder<IResource>> AddMeilisearch(
        IDistributedApplicationBuilder builder,
        [ResourceName] string name)
    {
        var masterkey = builder.AddParameter("masterkey", secret: true);
        //#if USE_LOCAL_MEILISEARCH
        // https://learn.microsoft.com/zh-cn/dotnet/aspire/community-toolkit/hosting-meilisearch?tabs=dotnet-cli#add-meilisearch-resource-with-data-bind-mount
        var meilisearchPath = GetMeilisearchPath();
        if (int.TryParse(MESSAGING_PORT, out var meilisearch_port))
        {
            meilisearch = builder.AddMeilisearch(name, masterkey, meilisearch_port);
        }
        else
        {
            meilisearch = builder.AddMeilisearch(name, masterkey);
        }
        meilisearch = meilisearch
            //.WithImageRegistry(imageRegistryUrl)
            //.WithImage("base/meilisearch", "latest")
            .WithImage("getmeili/meilisearch", "v1.21")
                                 .WithDataBindMount(
                                     source: meilisearchPath);
        return meilisearch;
        //#else
        //        // 使用常量添加参数，连接远程 Meilisearch 服务
        //        var masterkey_value = await masterkey.Resource.GetValueAsync(CancellationToken.None);
        //        ArgumentNullException.ThrowIfNull(masterkey_value);

        //        var nameUpper = name.ToUpperInvariant();
        //        var connectionStrings = $"Endpoint=http://{MESSAGING_HOST}:{MESSAGING_PORT};MasterKey={masterkey_value}";
        //        Dictionary<string, string> env = new()
        //                {
        //                    { $"{nameUpper}_HOST", MESSAGING_HOST },
        //                    { $"{nameUpper}_PORT", MESSAGING_PORT },
        //                    { $"{nameUpper}_MASTERKEY", masterkey_value },
        //                    { $"{nameUpper}_URI", $"http://{MESSAGING_HOST}:{MESSAGING_PORT}" },
        //                    { $"ConnectionStrings__{name}", connectionStrings },
        //                };
        //        var rabbitmq = builder.AddParameter(name, ConnectionStrings__cache, secret: true);
        //        return new EnvDictResourceBuilder(rabbitmq, env);
        //#endif
    }

    internal static IResourceBuilder<TDestination> WithMeilisearch<TDestination>(
        this IResourceBuilder<TDestination> builder
    )
        where TDestination : IResourceWithEnvironment
    {
        if (meilisearch_p != null)
        {
            builder.WithEnvironment($"{ConnectionStringEnvironmentName}{meilisearch_p.Resource.Name}", meilisearch_p);
        }
        else if (meilisearch != null)
        {
            builder.WithReference(meilisearch);
        }
        return builder;
    }
}

/// <summary>
/// 使用字典模拟环境变量的资源构建器
/// <para>https://learn.microsoft.com/zh-cn/dotnet/aspire/fundamentals/external-parameters</para>
/// </summary>
file sealed class EnvDictResourceBuilder(IResourceBuilder<ParameterResource> resourceBuilder, Dictionary<string, string> environment) : IResourceBuilder<ParameterResource>
{
    public IDistributedApplicationBuilder ApplicationBuilder => resourceBuilder.ApplicationBuilder;

    public ParameterResource Resource => resourceBuilder.Resource;

    public IResourceBuilder<ParameterResource> WithAnnotation<TAnnotation>(TAnnotation annotation, ResourceAnnotationMutationBehavior behavior = ResourceAnnotationMutationBehavior.Append) where TAnnotation : IResourceAnnotation
    {
        return resourceBuilder.WithAnnotation(annotation, behavior);
    }

    public IReadOnlyDictionary<string, string> Environment => environment;
}
