using Domain.Core.Entities.Interface;

namespace Domain.Core.Entities;

public class EntityBaseAudited : Entity, IBaseAudited
{
    public DateTimeOffset? CreationTime { get; set; }
    public string? CreatorUserId { get; set; }
}

