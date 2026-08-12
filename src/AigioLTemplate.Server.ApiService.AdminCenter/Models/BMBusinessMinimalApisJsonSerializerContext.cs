using AigioL.Common.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AigioLTemplate.ApiService.Models;

[JsonSerializable(typeof(ApiRsp))]
[JsonSourceGenerationOptions]
public sealed partial class BMBusinessMinimalApisJsonSerializerContext : JsonSerializerContext // 管理后台（业务）共享的模型类的源生成上下文
{
    static BMBusinessMinimalApisJsonSerializerContext()
    {
        JsonSerializerOptions o = new();
        IJsonSerializerContext.SetDefaultOptions(o);
        Default = new BMBusinessMinimalApisJsonSerializerContext(o);
    }
}