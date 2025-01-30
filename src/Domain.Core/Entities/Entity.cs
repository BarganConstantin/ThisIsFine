using Domain.Core.Entities.Base;

namespace Domain.Core.Entities;

public abstract class Entity : EntityBase<string>
{
    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
