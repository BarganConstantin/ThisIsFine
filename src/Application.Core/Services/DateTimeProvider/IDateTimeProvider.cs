namespace Application.Core.Services.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTimeOffset UtcNow { get; }
    }
}
