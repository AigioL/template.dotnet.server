using AigioL.Common.AspNetCore.AdminCenter.Constants;
using AigioL.Common.AspNetCore.AdminCenter.Entities;
using AigioL.Common.AspNetCore.AdminCenter.Services.Abstractions;
using AigioLTemplate.Server.ApiService.AdminCenter.Models;

namespace AigioLTemplate.Server.ApiService.AdminCenter.Services;

sealed class AdminCenterService : IAdminCenterService
{
    public Type RoleEnumType => typeof(BMRoleEnum);

    public Guid RootTenantIdG => TenantConstants.RootTenantIdG;

    public string RoleNameAdministrator => nameof(BMRoleEnum.Administrator);

    public List<string> AddRoles => [.. Enum.GetValues<BMRoleEnum>().Select(x => x.ToString()).Where(x => x != RoleNameAdministrator)];

    public void HandleMenus(bool isRootTenant, List<BMMenu> menus)
    {
    }
}