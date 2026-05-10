using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record ReleaseYear(int Value) : ValueObject
{
    public static ReleaseYear Create(int value)
    {
        Asserts.EnsureGreaterThan(value, 1800);
        return new ReleaseYear(value);
    }
}
