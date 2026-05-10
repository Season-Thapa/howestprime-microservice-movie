using System.Runtime.CompilerServices;

namespace Howestprime.Movies.Domain.Shared;

public static class Asserts
{
    public static void EnsureNotEmpty(
        string value,
        [CallerArgumentExpression(nameof(value))] string? paramName = ""
    )
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty.", paramName);
    }
    public static void EnsureNotEmpty(
        object value,
        [CallerArgumentExpression(nameof(value))] string? paramName = ""
    )
    {
        if (value == null)
            throw new ArgumentException("Value cannot be empty.", paramName);
    }

    public static void EnsureGreaterThan(
        int value,
        int threshold,
        [CallerArgumentExpression(nameof(value))] string? paramName = ""
    )
    {
        if (value <= threshold)
            throw new ArgumentException($"Value must be greater than {threshold}.", paramName);
    }

    public static void EnsureNotNegative(
        int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = ""
    )
    {
        if (value < 0)
            throw new ArgumentException("Value cannot be negative.", paramName);
    }
    public static void EnsureShowtimeIsAt15hOr19h(DateTime time)
    {
        if (time.Hour != 15 && time.Hour != 19)
            throw new ArgumentException("Time must be at 15h or 19h.", nameof(time));
    }

    public static void EnsureShowtimeIsInTheFuture(DateTime time)
    {
        if (time <= DateTime.Now)
            throw new ArgumentException($"Movie event {time} must be in the future.");
    }
}
