using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies;
public sealed record Actor(string Name) : ValueObject
{
    public string Name { get; init; } = Name;
    public static Actor Create(string name)
    {
        Asserts.EnsureNotEmpty(name);
        return new Actor(name);
    }
}
