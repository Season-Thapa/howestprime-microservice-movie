using Xunit;
namespace IntegrationTest.Shared;

[CollectionDefinition("MovieApi")]
public sealed class MovieApiCollection : ICollectionFixture<MovieApiFixture>;
