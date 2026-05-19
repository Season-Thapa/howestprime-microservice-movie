using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Domain.MovieEvents;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record HowestprimeSchedule
{
    public IReadOnlyList<Guid> MovieIds { get; set; } = [];
    public IReadOnlyList<MovieEventData> MovieEvents { get; set; } = [];

}