using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Shared;

namespace UnitTests.Application.Mocks;

public sealed class FakeUnitOfWork : IUnitOfWork
{
    private readonly Dictionary<Type, IRepository> _repositories = new();

    public int DoCalls { get; private set; }
    public int SaveCalls { get; private set; }
    public IAggregateRoot? SavedAggregateRoot { get; private set; }

    public void SetRepository<TRepository>(TRepository repository) where TRepository : IRepository
    {
        _repositories[typeof(TRepository)] = repository;
    }

    public Task Do()
    {
        DoCalls++;
        return Task.CompletedTask;
    }

    public Task Save<TRepository>(IAggregateRoot aggregateRoot) where TRepository : IRepository
    {
        SaveCalls++;
        SavedAggregateRoot = aggregateRoot;
        return Task.CompletedTask;
    }

    public TRepository Repo<TRepository>() where TRepository : IRepository
    {
        return (TRepository)_repositories[typeof(TRepository)];
    }
}