using GitHubApiClient;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets("3d461c20-a88b-4c45-a0f2-2b891e09d5fd")
    .Build();

var client = new GitHubClient("adamfoneil", config["GitHub:Token"]);
var results = await client.GetIssuesAsync("Hs5");

foreach (var issue in results)
{
    Console.WriteLine(issue.title);
}
