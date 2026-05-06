using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record ReleaseYear(int Value) : ValueObject
{
    public static ReleaseYear Create(int value)
    {
       Asserts.EnsureGreaterThan(DateTime.Now.Year + 1, value);
       Asserts.EnsureNotNegative(value);
        return new ReleaseYear(value);
    }
}
