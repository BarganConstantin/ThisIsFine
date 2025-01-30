using Domain.Core.Entities.Interface;

namespace Domain.Core.Entities;

public class EntityFullAudited : EntityAudited, IFullAudited
{
    public bool IsDeleted { get; set; }

    public string? DeleterUserId { get; set; }
    public DateTimeOffset? DeletionTime { get; set; }
}
