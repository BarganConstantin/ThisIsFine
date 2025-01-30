using Microsoft.AspNetCore.Mvc;
using ThiIsFine.Application.Usages.Base;

namespace Web.Models;

public class UsageViewModel
{
    public string? Id { get; private set; }
    public string? UserId { get; private set; }
    public string? PurchaseId { get; private set; }
    public string? SubscriptionName { get; private set; }
    public string? ImageId { get; private set; }
    public string? ImageUrl { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    
    public static UsageViewModel Create(UsageDto usageDto, IUrlHelper url, HttpContext httpContext)
    {
        return new UsageViewModel
        {
            Id = usageDto.Id,
            UserId = usageDto.UserId,
            PurchaseId = usageDto.PurchaseId,
            SubscriptionName = usageDto.SubscriptionName,
            ImageId = usageDto.ImageId,
            ImageUrl =  url.Action("GetImage", "Image", new { id = usageDto.ImageId }, httpContext.Request.Scheme),
            CreatedAt = usageDto.CreatedAt
        };
    }
}
