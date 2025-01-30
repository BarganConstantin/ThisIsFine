using Domain.Core.Entities.Interface;

namespace Domain.Core.Entities;

public class EntityAudited : EntityBaseAudited, IAudited
{
    public string? LastModifierUserId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
}
