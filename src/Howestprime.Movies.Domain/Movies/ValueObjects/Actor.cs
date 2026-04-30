using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;

public sealed record Actor : ValueObject
{
    public string Value { get; }

    private Actor(string value)
    {
        Value = value;
    }

    public static Actor Create(string value)
    {
        Asserts.EnsureNotEmpty(value);
        return new Actor(value);
    }
}

