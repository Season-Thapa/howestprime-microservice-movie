using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace IntegrationTest.Shared;

public sealed class MovieApiFixture : IAsyncLifetime
{
    private static readonly MovieTestSettings Settings = new(
        DatabaseName: "movie",
        DatabaseUsername: "movie",
        DatabasePassword: "movie",
        RabbitUsername: "movie",
        RabbitPassword: "movie"
    );

    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .WithDatabase(Settings.DatabaseName)
        .WithUsername(Settings.DatabaseUsername)
        .WithPassword(Settings.DatabasePassword)
        .Build();

    private readonly RabbitMqContainer _rabbitMq = new RabbitMqBuilder()
        .WithImage("rabbitmq:3.13-management-alpine")
        .WithUsername(Settings.RabbitUsername)
        .WithPassword(Settings.RabbitPassword)
        .Build();

    private MovieApiFactory? _factory;

    public MovieApiClient Client { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        await _rabbitMq.StartAsync();

        _factory = new MovieApiFactory(_postgres, _rabbitMq, Settings);
        Client = new MovieApiClient(_factory.CreateClient());
    }

    public async Task DisposeAsync()
    {
        if (_factory is not null)
            _factory.Dispose();

        await _rabbitMq.DisposeAsync();
        await _postgres.DisposeAsync();
    }
}