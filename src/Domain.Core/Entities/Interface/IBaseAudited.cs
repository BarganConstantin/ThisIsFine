namespace Domain.Core.Entities.Interface;

public interface IBaseAudited : IHasKey<string>
{
    DateTimeOffset? CreationTime { get; set; }
    string? CreatorUserId { get; set; }
}

