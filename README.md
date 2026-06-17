## 项目模板（服务端）
使用 [.NET Aspire（阿斯拜尔）](https://dotnet.microsoft.com/zh-cn/apps/cloud)构建的云原生应用。

| Type                         | Development                                                                                    | Production                                                                             |
| ---------------------------- | ---------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------- |
| Dashboard                    | [aigioltemplate-d.aigiol.github.io](https://aigioltemplate-d.aigiol.github.io)                                           | [aigioltemplate-d.aigiol.github.io](https://aigioltemplate-d.aigiol.github.io)                                           |
| Web Frontend(Admin Center)   | [aigioltemplate-admin.aigiol.github.io](https://aigioltemplate-admin.aigiol.github.io)                                   | [aigioltemplate-admin.aigiol.github.io](https://aigioltemplate-admin.aigiol.github.io)                                   |
| Web Api(Admin Center)        | [aigioltemplate-admin-api.speedtest.aigiol.github.io](https://aigioltemplate-admin-api.speedtest.aigiol.github.io)       | [aigioltemplate-admin-api.speedtest.aigiol.github.io](https://aigioltemplate-admin-api.speedtest.aigiol.github.io)       |
| Web Api(Microservices)       | [aigioltemplate-api.speedtest.aigiol.github.io](https://aigioltemplate-api.speedtest.aigiol.github.io)                   | [aigioltemplate-api.speedtest.aigiol.github.io](https://aigioltemplate-api.speedtest.aigiol.github.io)                   |
| CDN                          | [aigioltemplate.speedtest.aigiol.github.io](https://aigioltemplate.speedtest.aigiol.github.io)                           | [aigioltemplate.speedtest.aigiol.github.io](https://aigioltemplate.speedtest.aigiol.github.io)                           |
| Official Website             | [aigioltemplate.aigiol.github.io:29005](https://aigioltemplate.aigiol.github.io:29005)                               | [aigioltemplate.aigiol.github.io](https://aigioltemplate.aigiol.github.io)                                               |

![eShop Reference Application architecture diagram](res/eshop_architecture.png)

### 模板重命名（重命名后删除此）
1. 编译 AigioLTemplate.Server.BuildTools
2. 运行命令
```
AigioLTemplate.Server.BuildTools.exe rename --projName 项目名称 --projNameLower 项目名称全小写 --notTrimServer true --webProtStart 19000
```

### 机密值存储位置
[使用 ASP.NET Core 安全地存储开发中的应用机密](https://learn.microsoft.com/zh-cn/aspnet/core/security/app-secrets)  

RSA 公钥(SecurityKey Web API)
```
TODO
```

#### AppHost
- Windows ```%APPDATA%\Microsoft\UserSecrets\{TODO_GUID}\secrets.json```
- Linux/macOS ```~/.microsoft/usersecrets/{TODO_GUID}/secrets.json```
```
{
  "Parameters:cache-password": "**********",
  "AppHost:OtlpApiKey": "**********",
  "Parameters:postgres-password": "**********"
}
```

### PostgreSQL 数据库表数据存储位置
```[ProjPath]\res\postgresql\data```

### [EF Core 迁移](https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/?tabs=vs)
截止 EF Core 9.0，迁移操作与 AOT 不兼容，执行操作时取消 csproj 末尾行中的
```
<!--<PublishAot>false</PublishAot>-->
```
避免错误
```
Unable to create a 'DbContext' of type 'AppDbContext'. The exception 'Model building is not supported when publishing with NativeAOT. Use a compiled model.' was thrown while ataigioltemplateting to create an instance. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
```

#### 创建迁移
```
Add-Migration {本次变更唯一名称} -Context AppDbContext
```

#### 更新迁移
```
Update-Database -Context AppDbContext
```

#### 生成 SQL 脚本
```
Script-Migration {上一次变更唯一名称} -Context AppDbContext
```

### Docker
仅打包所有微服务镜像命令
```
AigioLTemplate.Server.BuildTools spub
```
仅推送镜像命令
```
AigioLTemplate.Server.BuildTools spub --push_only --push_name aigioltemplate --push_domain docker.aigiol.github.io:10001
```

#### Dockerfile(Native AOT)
```
string AigioLTemplate.Server.UnitTest.DockerfileTest.dockerfile_content_template
```