using AigioL.Common.AspNetCore.AppCenter.Ordering.Services.Abstractions;
using AigioLTemplate.Models;

namespace AigioLTemplate.Services;

public sealed partial class OrderBusinessTypeService : IOrderBusinessTypeService
{
    public int Membership => (int)OrderBusinessType.Membership;
}
