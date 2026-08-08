using AigioL.Common.AspNetCore.AppCenter.Ordering.Entities.Membership;
using System.ComponentModel;

namespace AigioLTemplate.Models;

/// <summary>
/// 业务订单类型
/// </summary>
public enum OrderBusinessType
{
    /// <summary>
    /// 会员充值、续费，包含支付与协议扣款以及 CDK 兑换
    /// <para>关联的业务订单表为 <see cref="MembershipBusinessOrder"/></para>
    /// </summary>
    [Description("会员")]
    Membership = 1,
}
