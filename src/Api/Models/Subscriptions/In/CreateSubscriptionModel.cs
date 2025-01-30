using ThiIsFine.Api.Models.Base;
using ThiIsFine.Application.Subscriptions.Commands.Create.DTOs;

namespace ThiIsFine.Api.Models.Subscriptions.In;

public sealed record CreateSubscriptionModel : IRequestModel<CreateSubscriptionCommand>
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int UsageLimit { get; set; }
    public required string Description { get; set; }
    
    public CreateSubscriptionCommand Convert() => 
        new(new CreateSubscriptionDto(Name, Price, UsageLimit, Description));
}
