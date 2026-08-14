using AigioL.Common.AspNetCore.AppCenter.Data.Abstractions;
using AigioL.Common.AspNetCore.Helpers.ProgramMain;
using AigioL.Common.AspNetCore.PartnerCenter.Data.Abstractions;
using AigioL.Common.AspNetCore.PartnerCenter.Entities;
using AigioL.Common.EntityFrameworkCore.Helpers;
using AigioL.Common.Repositories.EntityFrameworkCore.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable IDE0130 // 命名空间与文件夹结构不匹配
namespace AigioLTemplate.ApiService.Data;

public sealed partial class AppDbContext
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

#if PROJ_DBCONTEXT_PC
        // 由于 PCDbContextBase 不继承自 ACUser 的 IdentityDbContext，故需要调用基类的 OnModelCreatingVersion2 方法
        IAppDbContextBase.OnModelCreatingVersion2(this, b);
#endif

        // 重命名 Identity AC 相关表名
        IAppDbContextBase.ToIdentitysTable(b);
    }
}

#if PROJ_DBCONTEXT_PC
partial class AppDbContext(
    DbContextOptions<AppDbContext> options,
    IHttpContextAccessor httpContextAccessor) :
    PCDbContextBase<PCUser, PCRole, PCUserRole>(options, httpContextAccessor);
#else
partial class AppDbContext(
    DbContextOptions<AppDbContext> options) :
    AppDbContextBase(options);
#endif

partial class AppDbContext : ProgramHelper.IDbContext
{
    /// <inheritdoc/>
    DbContext ProgramHelper.IDbContext.GetDbContext() => this;
}

partial class AppDbContext : IDbContextBase
{
    /// <inheritdoc/>
    DbContext IDbContextBase.GetDbContext() => this;

    /// <inheritdoc/>
    DatabaseFacade IDbContextBase.GetDatabase() => Database;
}

partial class AppDbContext : IIdentityDbContext<PCUser, PCRole, Guid, PCUserClaim, PCUserRole, PCUserLogin, PCRoleClaim, PCUserToken>;

/// <summary>
/// https://learn.microsoft.com/zh-cn/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-a-design-time-factory
/// </summary>
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public const bool postgreSQL18Plus = true;

    /// <inheritdoc/>
    public AppDbContext CreateDbContext(string[] args)
    {
        SqlStringHelper.ConfigPostgreSQL(postgreSQL18Plus);

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("");

#if PROJ_DBCONTEXT_PC
        IHttpContextAccessor httpContextAccessor = null!; // 设计时不需要 HttpContext
        return new AppDbContext(optionsBuilder.Options, httpContextAccessor);
#else
        return new AppDbContext(optionsBuilder.Options);
#endif
    }
}