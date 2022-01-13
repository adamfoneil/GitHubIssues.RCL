using GitHubApiClient;
using GitHubApiClient.Models.Requests;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var config = new ConfigurationBuilder()
    .AddUserSecrets("3d461c20-a88b-4c45-a0f2-2b891e09d5fd")
    .Build();

var client = new GitHubClient("adamfoneil", config["GitHub:Token"]);
//var results = await client.GetIssuesAsync("Hs5");
var results = await client.GetIssuesAsync("Hs5", new IssuesQuery()
{
    State = IssueState.Open,
    Since = DateTime.Today.AddDays(-5)    
});

foreach (var issueGrp in results.GroupBy(item => item.assignee?.login ?? "(unassigned)"))
{
    Console.WriteLine(issueGrp.Key);
    foreach (var issue in issueGrp)
    {
        Console.WriteLine($"- {issue.number}: {issue.title}");        
    }    
    Console.WriteLine();
}
