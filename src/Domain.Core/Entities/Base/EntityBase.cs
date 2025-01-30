using Domain.Core.Entities.Interface;

namespace Domain.Core.Entities.Base;

public abstract class EntityBase<T> : IHasKey<T>
{
    public virtual T Id { get; set; } = default!;
}

