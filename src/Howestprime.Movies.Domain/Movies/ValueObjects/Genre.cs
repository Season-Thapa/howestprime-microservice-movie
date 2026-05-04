using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;

public sealed record Genre(string Value) : ValueObject
{

    public static Genre Create(string value)
    {
        Asserts.EnsureNotEmpty(value);
        return new Genre(value);
    }
}
