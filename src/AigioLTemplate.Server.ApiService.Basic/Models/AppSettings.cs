using AigioL.Common.AspNetCore.AppCenter.Basic.Models.Abstractions;
using AigioL.Common.AspNetCore.AppCenter.Models;

namespace AigioLTemplate.Server.ApiService.Basic.Models;

public sealed partial class AppSettings : MSAppSettings, IAppSettings
{
    /// <inheritdoc/>
    public string? ImageUrl { get; set; }

    /// <inheritdoc/>
    public string? OfficialWebsite { get; set; }

    #region COS 云存储配置

    /// <summary>
    /// 云存储 每次请求签名有效时长，单位为秒
    /// </summary>
    public int COSDurationSecond { get; set; } = 600;

    /// <summary>
    /// 云存储 云 API 密钥 SecretId, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
    /// </summary>
    public string? COSSecretId { get; set; }

    /// <summary>
    /// 云储存 云 API 密钥 SecretKey, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
    /// </summary>
    public string? COSSecretKey { get; set; }

    /// <summary>
    /// 图片 云存储区域
    /// </summary>
    public string ImageHandleCosRegion { get; set; } = "";

    /// <summary>
    /// 图片 存储桶名称
    /// </summary>
    public string ImageHandleCosBucket { get; set; } = "";

    #endregion
}
