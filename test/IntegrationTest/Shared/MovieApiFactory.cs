using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace IntegrationTest.Shared;

public sealed class MovieApiFactory(
    PostgreSqlContainer postgres,
    RabbitMqContainer rabbitMq,
    MovieTestSettings settings
    
    ) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureAppConfiguration((_, config) =>
        {
            string connectionString =
                $"Host={postgres.Hostname};" +
                $"Port={postgres.GetMappedPublicPort(5432)};" +
                $"Database={settings.DatabaseName};" +
                $"Username={{username}};" +
                $"Password={{password}}";

            var configOverrides = new Dictionary<string, string?>
            {
                ["Database:Provider"] = "PostgreSQL",
                ["Database:ConnectionString"] = connectionString,
                ["Database:Username"] = settings.DatabaseUsername,
                ["Database:Password"] = settings.DatabasePassword,
                ["MessageBroker:AsyncApiPath"] = TestPaths.AsyncApiSpecPath,
                ["MessageBroker:Hostname"] = rabbitMq.Hostname,
                ["MessageBroker:Port"] = rabbitMq.GetMappedPublicPort(5672).ToString(),
                ["MessageBroker:Username"] = settings.RabbitUsername,
                ["MessageBroker:Password"] = settings.RabbitPassword,
                ["MessageBroker:VirtualHost"] = "%2f"
            };

            config.AddInMemoryCollection(configOverrides);
        });
    }
}