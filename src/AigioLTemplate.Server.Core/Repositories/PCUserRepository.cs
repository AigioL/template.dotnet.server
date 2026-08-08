using AigioL.Common.AspNetCore.AdminCenter.PartnerCenter.Models;
using AigioL.Common.AspNetCore.PartnerCenter.Entities;
using AigioL.Common.AspNetCore.PartnerCenter.Models;

#pragma warning disable IDE0130 // 命名空间与文件夹结构不匹配
namespace AigioL.Common.AspNetCore.AdminCenter.PartnerCenter.Repositories;

partial class PCUserRepository<TDbContext>
{
    public partial async Task<PCUserOrderFilterModel> BuildOrderFilterAsync(
        PCUser user,
        CancellationToken cancellationToken)
    {
        var businessIds = user.BusinessIds
            .Where(static x => x != default)
            .Distinct()
            .ToArray();

        if (businessIds.Length == 0)
        {
            return new PCUserOrderFilterModel
            {
                UserType = user.UserType,
                BusinessIds = [],
            };
        }

        if (user.UserType == PCUserType.Channel)
        {
            return new PCUserOrderFilterModel
            {
                UserType = PCUserType.Channel,
                BusinessIds = businessIds,
            };
        }

        return new PCUserOrderFilterModel
        {
            UserType = user.UserType,
            BusinessIds = businessIds,
        };
    }
}
