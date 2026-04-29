using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;

public sealed record Actor : ValueObject
{
    public string Name { get; }

    private Actor(string name)
    {
        Name = name;
    }

    public static Actor Create(string name)
    {
        Asserts.EnsureNotEmpty(name);
        return new Actor(name);
    }
}

