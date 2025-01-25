namespace Dunnhumby.API.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUTCDate() => DateTime.UtcNow;
}
