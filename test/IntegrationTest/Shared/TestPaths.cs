namespace IntegrationTest.Shared;

public static class TestPaths
{
    public static string AsyncApiSpecPath => Path.Combine(RepoRootPath, "config", "asyncapi.yaml");

    public static string RepoRootPath => FindRepoRoot(AppContext.BaseDirectory);

    private static string FindRepoRoot(string startPath)
    {
        string? current = startPath;
        while (!string.IsNullOrWhiteSpace(current))
        {
            if (File.Exists(Path.Combine(current, "Howestprime.Movies.slnx")))
                return current;

            current = Directory.GetParent(current)?.FullName;
        }

        throw new DirectoryNotFoundException("Unable to locate repository root from test base directory.");
    }
}
