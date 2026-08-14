using AigioL.Common.AspNetCore.AdminCenter.Models;
using AigioL.Common.AspNetCore.AppCenter.Models.Abstractions;
using AigioL.Common.SmsSender.Models;
using AigioL.Common.SmsSender.Models.Abstractions;

namespace AigioLTemplate.ApiService.PartnerCenter.Models;

public sealed partial class AppSettings : BMAppSettings
{
}

partial class AppSettings : IDisableSms
{
    /// <inheritdoc/>
    public bool DisableSms { get; set; }
}

partial class AppSettings : ISmsSettings
{
    /// <inheritdoc/>
    public bool? UseDebugSmsSender { get; set; }

    /// <inheritdoc/>
    public SmsOptions? SmsOptions { get; set; }
}
