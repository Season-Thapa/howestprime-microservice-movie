using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record PosterUrl(string Value) : ValueObject
{
    public string Value { get; init; } = Value;
    public static PosterUrl Create(string value)
    {
        Asserts.EnsureNotEmpty(value);
        return new PosterUrl(value);
    }
}
