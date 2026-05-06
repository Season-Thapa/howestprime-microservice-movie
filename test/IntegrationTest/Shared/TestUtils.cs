using System.Net.Http.Json;
using System.Text.Json;

namespace IntegrationTest.Shared;

public static class TestUtils
{
    public static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    public static Guid ExtractIdFromLocation(Uri? location)
    {
        if (location is null)
            throw new InvalidOperationException("Response did not contain a Location header.");

        string path = location.IsAbsoluteUri ? location.AbsolutePath : location.OriginalString;
        string lastSegment = path.TrimEnd('/').Split('/').Last();
        return Guid.Parse(lastSegment);
    }

    public static async Task<T> ReadAsAsync<T>(HttpResponseMessage response)
    {
        T? result = await response.Content.ReadFromJsonAsync<T>(JsonOptions);
        if (result is null)
            throw new InvalidOperationException("Response body was empty.");

        return result;
    }
}
