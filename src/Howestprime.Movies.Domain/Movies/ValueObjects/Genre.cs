using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;

public sealed record Genre : ValueObject
{
    public string Name { get; }

    private Genre(string name)
    {
        Name = name;
    }

    public static Genre Create(string name)
    {
        Asserts.EnsureNotEmpty(name);
        return new Genre(name);
    }
}
