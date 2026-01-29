using AigioL.Common.AspNetCore.AppCenter.Models;
using AigioL.Common.AspNetCore.AppCenter.Payment.Models;
using AigioL.Common.AspNetCore.AppCenter.Payment.Models.Abstractions;

namespace AigioLTemplate.Server.ApiService.Payment.JobScheduler.Models;

public sealed partial class AppSettings : MSAppSettings, IWeChatApiAppSettings
{
    public WeChatApiOptions WeChatApiOptions { get; set; } = new();

    IWeChatApiOptions IWeChatApiAppSettings.WeChatApiOptions => WeChatApiOptions;
}
