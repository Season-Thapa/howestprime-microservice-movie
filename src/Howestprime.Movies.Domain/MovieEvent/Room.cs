using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent;
public readonly record struct RoomId(Guid Value) : IEntityId;

public sealed class Room : AggregateRoot<RoomId>
{
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    private Room(RoomId id, string name, int capacity) : base(id)
    {
        Name = name;
        Capacity = capacity;
    }
    public static Room Create(string name, int capacity, RoomId? id = null)
    {
        Room room = new (
            id ??  EntityId.New<RoomId>(),
            name, 
            capacity
            );

        room.ValidateState();

        return room;
    }

    public override void ValidateState()
    {
        Asserts.EnsureNotEmpty(Name);
        Asserts.EnsureGreaterThan(Capacity, 0);
    }
  
}