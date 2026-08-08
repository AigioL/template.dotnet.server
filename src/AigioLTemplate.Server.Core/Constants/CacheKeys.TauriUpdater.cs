using System.Text;

#pragma warning disable IDE0130 // 命名空间与文件夹结构不匹配
namespace AigioL.Common.AspNetCore.AppCenter.Constants;

static partial class CacheKeys // aigioltemplate Tauri
{
    public const string TauriUpdaterLatestVersion = "最新版本";

    public const string TauriUpdaterDefaultDownloadUrl = "默认下载地址";

    public const string TauriUpdaterRuntimeCacheStamp = "TauriUpdater:RuntimeCacheStamp";

    public const int TauriUpdaterRuntimeCacheTimeoutMinutes = 60;

    public static string GetTauriUpdaterStaticJsonCacheKey(string target, string arch, string packageIdentifier, string stamp)
        => $"TauriUpdater:StaticJson:{target}:{arch}:{packageIdentifier}:{stamp}";

    public static string GetTauriUpdaterRedirectUrlCacheKey(string target, string arch, string packageIdentifier, string stamp)
        => $"TauriUpdater:RedirectUrl:{target}:{arch}:{packageIdentifier}:{stamp}";
}
