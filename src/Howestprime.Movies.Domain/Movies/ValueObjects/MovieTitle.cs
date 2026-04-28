using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record MovieTitle(string Value) : ValueObject
{
    public string Value { get; init; } = Value;
    public static MovieTitle Create(string value)
    {
        Asserts.EnsureNotEmpty(value);
        return new MovieTitle(value);
    }
}
