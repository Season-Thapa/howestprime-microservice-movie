using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record MovieTitle(string Value) : ValueObject
{
    public static MovieTitle Create(string value)
    {
        Asserts.EnsureNotEmpty(value);
        return new MovieTitle(value);
    }
}
