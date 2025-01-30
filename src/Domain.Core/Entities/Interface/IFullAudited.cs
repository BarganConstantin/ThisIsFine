namespace Domain.Core.Entities.Interface;

public interface IFullAudited : IAudited
{
    string? DeleterUserId { get; set; }
    DateTimeOffset? DeletionTime { get; set; }
}
