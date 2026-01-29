using AigioL.Common.AspNetCore.AppCenter.Models;
using AigioL.Common.AspNetCore.AppCenter.Payment.Models;
using AigioL.Common.AspNetCore.AppCenter.Payment.Models.Abstractions;

namespace AigioLTemplate.Server.ApiService.Payment.Models;

public sealed partial class AppSettings : MSAppSettings, IAppSettings
{
    public WeChatApiOptions WeChatApiOptions { get; set; } = new();

    IWeChatApiOptions IWeChatApiAppSettings.WeChatApiOptions => WeChatApiOptions;

    public bool DebugOnlinePayment { get; set; }

    public string OfficialUrl
    {
        get
        {
            ArgumentNullException.ThrowIfNull(field);
            return field;
        }
        set;
    }
}
