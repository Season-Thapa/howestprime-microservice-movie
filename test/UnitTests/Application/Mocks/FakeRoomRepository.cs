using Aornis;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Application.Mocks
{
    public sealed class FakeRoomRepository : IRoomRepository
    {
        private readonly HashSet<Room> _rooms = [];
        public Task<Optional<Room>> ById(RoomId id)
        {
            return Task.FromResult(
           Optional.Of(_rooms.FirstOrDefault(r => r.Id.Equals(id)))
       );
        }

        public Task<bool> Exists(RoomId id)
        {
            return Task.FromResult(
                _rooms.Any(r => r.Id.Equals(id))
            );
        }

        public Task Remove(Room aggregateRoot)
        {
            return Task.FromResult(
                _rooms.Remove(aggregateRoot)
            );
        }

        public Task Save(Room aggregateRoot)
        {
            _rooms.Add(aggregateRoot);
            return Task.CompletedTask;
        }
    }
}
