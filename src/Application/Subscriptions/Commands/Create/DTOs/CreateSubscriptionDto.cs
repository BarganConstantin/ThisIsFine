namespace ThiIsFine.Application.Subscriptions.Commands.Create.DTOs;

public sealed record CreateSubscriptionDto(string Name, decimal Price, int UsageLimit, string Description);
