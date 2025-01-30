using Domain.Core.Entities;
using ThiIsFine.Domain.Entities.Images;

namespace ThiIsFine.Domain.Entities.Usages;

public class Usage : EntityFullAudited
{
    public string? UserId { get; private set; }
    
    public string? PurchaseId { get; private set; }
    public virtual Purchases.Purchase? Purchase { get; private set; }
    
    public string? ImageId { get; private set; }
    public AspNetImage? Image { get; private set; }
    
    public static Usage Create(string userId, string? purchaseId, string imageId)
    {
        return new Usage
        {
            UserId = userId,
            PurchaseId = purchaseId,
            ImageId = imageId
        };
    }
}
