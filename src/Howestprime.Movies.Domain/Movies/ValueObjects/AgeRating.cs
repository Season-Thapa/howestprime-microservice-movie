using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record AgeRating(int Value) : ValueObject
{
    public static AgeRating Create(int value)
    {
        Asserts.EnsureGreaterThan(value, 0);
        return new AgeRating(value);
    }
}
