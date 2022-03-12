using GitHubApiClient;
using GitHubApiClient.Models.Requests;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets("3d461c20-a88b-4c45-a0f2-2b891e09d5fd")
    .Build();

var client = new GitHubClient("adamfoneil", config["GitHub:Token"]);
//var results = await client.GetIssuesAsync("Hs5");
var results = await client.GetAllIssuesAsync("Hs5", new IssuesQuery()
{
    State = IssueState.Open
});

foreach (var issueGrp in results.GroupBy(item => item.assignee?.login ?? "(unassigned)"))
{
    Console.WriteLine(issueGrp.Key);
    foreach (var issue in issueGrp)
    {
        var labels = string.Join(", ", issue.labels.Select(l => $"[ {l.name} ]"));
        Console.WriteLine($"- {issue.number}: {issue.title} {labels}");
    }
    Console.WriteLine();
}
