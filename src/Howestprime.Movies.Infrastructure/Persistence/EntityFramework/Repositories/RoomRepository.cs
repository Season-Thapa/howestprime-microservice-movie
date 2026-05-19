using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories
{
    public sealed class RoomRepository(DomainDbContext context) : EfCoreGenericRepository<Room, RoomId>(context), IRoomRepository
    {

    }
}
