using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
public sealed record Duration(int Value) : ValueObject
{
    public int Value { get; init; } = Value;
    public static Duration Create(int value)
    {
        Asserts.EnsureGreaterThan(value, 0);
        return new Duration(value);
    }
}