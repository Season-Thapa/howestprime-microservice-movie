namespace IntegrationTest.Shared;

public sealed record MovieTestSettings
(
    string DatabaseName,
    string DatabaseUsername,
    string DatabasePassword,
    string RabbitUsername,
    string RabbitPassword
);