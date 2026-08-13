using AigioL.Common.AspNetCore.AppCenter.Analytics.Data.Abstractions;
using AigioL.Common.AspNetCore.AppCenter.Ordering.Entities.Summaries;
using Microsoft.EntityFrameworkCore;

#pragma warning disable IDE0130 // 命名空间与文件夹结构不匹配
namespace AigioLTemplate.ApiService.Data;

partial class AppDbContext : IOrderSummariesDbContext
{
    public DbSet<OrderAmountQtySummary> OrderAmountQtySummaries { get; set; } = null!;
}
