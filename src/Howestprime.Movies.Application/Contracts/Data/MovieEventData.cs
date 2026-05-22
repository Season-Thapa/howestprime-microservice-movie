using System.Text.Json.Serialization;

namespace Howestprime.Movies.Application.Contracts.Data;

public sealed record MovieEventData
{
    public Guid Id { get; set; }
    [JsonPropertyName("showtime")]
    public DateTime Showtime { get;  set; }
    public int Capacity { get;  set; }
    public RoomData Room { get; set; }
    public MovieData Movie { get; set; }
}
