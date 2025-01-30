using Application.Core.Services.DateTimeProvider;

namespace ThiIsFine.Infrastructure.Services.DateTimeProvider
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset UtcNow
        {
            get { return DateTimeOffset.UtcNow; }
        }
    }
}
