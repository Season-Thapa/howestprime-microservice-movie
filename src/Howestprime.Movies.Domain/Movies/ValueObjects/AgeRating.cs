using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record ReleaseYear(int Value) : ValueObject
{
    public int Value { get; init; } = Value;
    public static ReleaseYear Create(int value)
    {
        Asserts.EnsureGreaterThan(value, 0);
        return new ReleaseYear(value);
    }
}
