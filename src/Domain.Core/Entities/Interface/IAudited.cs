namespace Domain.Core.Entities.Interface;

public interface IAudited : IBaseAudited
{
    string? LastModifierUserId { get; set; }
    DateTimeOffset? LastModificationTime { get; set; }
}
