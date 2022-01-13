using GitHubApiClient;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets("3d461c20-a88b-4c45-a0f2-2b891e09d5fd")
    .Build();

var httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
httpClient.BaseAddress = new Uri("https://api.github.com");

var client = new GitHubClient("adamfoneil", config["GitHub:Token"]);
var results = await client.GetIssuesAsync("Hs5");

foreach (var issue in results)
{
    Console.WriteLine(issue.title);
}


/// <summary>
/// help from https://stackoverflow.com/a/18925296/2023653
/// </summary>
class LoggingHandler : DelegatingHandler
{
    public LoggingHandler(HttpMessageHandler handler) : base(handler)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.ToString());
        if (request.Content != null)
        {
            Console.WriteLine(await request.Content.ReadAsStringAsync());
        }

        return await base.SendAsync(request, cancellationToken);
    }
}