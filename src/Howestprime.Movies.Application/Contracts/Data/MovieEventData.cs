namespace Howestprime.Movies.Application.Contracts.Data;

public sealed record MovieEventData
{
    public Guid Id { get; set; }
    public RoomData Room { get; set; }
    public MovieData Movie { get; set; }
    public DateTime ShowTime { get;  set; }
    public int Capacity { get;  set; }
}