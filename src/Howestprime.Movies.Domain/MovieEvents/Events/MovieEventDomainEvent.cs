using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvents.Events;

public class MovieEventDomainEvent(
    string eventName) : BaseDomainEvent(
    eventName,
    aggregateName: nameof(MovieEvent),
    companyName: nameof(Howestprime))
{
    
}