using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record ReleaseYear(int Value) : ValueObject
{
    public static ReleaseYear Create(int value)
    {
        Asserts.EnsureNotNegative(value);
        if (value > DateTime.UtcNow.Year)
            throw new ArgumentException("Value cannot be in the future.");
        return new ReleaseYear(value);
    }
}
