namespace Domain.Core.Entities.Interface;

public interface IHasKey<out T>
{
    T Id { get; }
}
