// Backend Service

public interface IUrlShortenerService
{
    string ShortenUrl(string longUrl, string customShortUrl = null);
    string GetLongUrl(string shortUrl);
    int GetClickCount(string shortUrl);
    void DeleteUrl(string shortUrl);
}

public class UrlShortenerService : IUrlShortenerService
{
    private readonly Dictionary<string, string> urlMappings = new Dictionary<string, string>();
    private readonly Dictionary<string, int> clickCounts = new Dictionary<string, int>();

    public string ShortenUrl(string longUrl, string customShortUrl = null)
    {
        string shortUrl = customShortUrl ?? GenerateShortUrl();
        urlMappings[shortUrl] = longUrl;
        clickCounts[shortUrl] = 0; // Initialize click count
        return shortUrl;
    }

    public string GetLongUrl(string shortUrl)
    {
        return urlMappings.ContainsKey(shortUrl) ? urlMappings[shortUrl] : null;
    }

    public int GetClickCount(string shortUrl)
    {
        return clickCounts.ContainsKey(shortUrl) ? clickCounts[shortUrl] : 0;
    }

    public void DeleteUrl(string shortUrl)
    {
        if (urlMappings.ContainsKey(shortUrl))
        {
            urlMappings.Remove(shortUrl);
            clickCounts.Remove(shortUrl);
        }
    }

    private string GenerateShortUrl()
    {
        const string urlPrefix = "tinyurl.com/";
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        string shortUrl;

        do
        {
            shortUrl = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        } while (urlMappings.ContainsKey(shortUrl)); // Generate until unique

        return urlPrefix + shortUrl;
    }
}