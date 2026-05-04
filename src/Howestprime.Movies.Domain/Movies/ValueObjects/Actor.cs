using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;

public sealed record Actor(string Value) : ValueObject
{

    public static Actor Create(string value)
    {
        Asserts.EnsureNotEmpty(value);
        return new Actor(value);
    }
}

