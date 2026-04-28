using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.Events
{
    public abstract class MovieDomainEvent(string eventName) : BaseDomainEvent(
        eventName,
        aggregateName: nameof(Movie)
    )
    {
    }
}
