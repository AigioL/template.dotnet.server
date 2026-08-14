using AigioL.Common.AspNetCore.AdminCenter.Constants;
using AigioL.Common.AspNetCore.AdminCenter.Entities;
using AigioL.Common.AspNetCore.AdminCenter.Services.Abstractions;
using AigioL.Common.AspNetCore.PartnerCenter.Entities;
using AntDesign;
#if PROJ_DBCONTEXT_BM
using TRoleEnum = AigioLTemplate.ApiService.AdminCenter.Models.BMRoleEnum;
#elif PROJ_DBCONTEXT_PC
using TRoleEnum = AigioLTemplate.ApiService.PartnerCenter.Models.PCRoleEnum;
#endif

namespace AigioLTemplate.ApiService.AdminCenter.Services;

sealed partial class AdminCenterService : IAdminCenterService
{
    public Type RoleEnumType => typeof(TRoleEnum);

    public Guid RootTenantIdG => TenantConstants.RootTenantIdG;

    public string RoleNameAdministrator => nameof(TRoleEnum.Administrator);

    public List<string> AddRoles => [.. Enum.GetValues<TRoleEnum>().Select(x => x.ToString()).Where(x => x != RoleNameAdministrator)];

    public void HandleMenus(bool isRootTenant, List<BMMenu> menus)
    {
#if PROJ_DBCONTEXT_BM
        menus.AddRange(GetBMMenus(isRootTenant));
#endif
    }

    public void HandleMenus(bool isRootTenant, List<PCMenu> menus)
    {
#if PROJ_DBCONTEXT_PC
        menus.AddRange(GetPCMenus(isRootTenant));
#endif
    }
}

partial class AdminCenterService
{
#if PROJ_DBCONTEXT_BM
    static IEnumerable<BMMenu> GetBMMenus(bool isRootTenant)
    {
        yield return new BMMenu
        {
            Url = "/TODO",
            Name = "TODO管理",
            Key = "TODO",
            IconUrl = IconType.Outline.Dash,
            Children = [],
        };
    }
#elif PROJ_DBCONTEXT_PC
    static IEnumerable<PCMenu> GetPCMenus(bool isRootTenant)
    {
        yield return new PCMenu
        {
            Url = "/TODO",
            Name = "TODO管理",
            Key = "TODO",
            IconUrl = IconType.Outline.Dash,
            Children = [],
        };
    }
#endif
}
