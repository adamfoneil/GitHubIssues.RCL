using GitHubApiClient;
using GitHubApiClient.Models.Requests;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets("3d461c20-a88b-4c45-a0f2-2b891e09d5fd")
    .Build();

var client = new GitHubClient("adamfoneil", config["GitHub:Token"]);

var events = (await client.GetAllEventsAsync("Hs5", 
    (qry, results) => results.Count(ev => ev.event_name.Equals("closed")) < 10))
    .Where(ev => ev.event_name.Equals("closed"))
    .Take(10);

/*
var repos = await client.GetAllMyRepositoriesAsync(new RepositoryQuery()
{
    Sort = RepoSortOptions.Pushed,
    Visiblity = VisiblityOptions.Private
});

int count = 0;
foreach (var repo in repos)
{
    count++;
    Console.WriteLine($"{repo.name} ({repo.visibility})\r\n\t- {repo.url}\r\n\t- {repo.id}\r\n\t- {repo.open_issues_count} issues\r\n\t- {repo.open_issues} other issues");
}

Console.WriteLine();
Console.WriteLine(count.ToString());

return;
*/

var closed = await client.GetIssuesAsync("Hs5", new IssuesQuery()
{
    State = IssueState.Closed,
    SortDirection = SortDirection.Descending
});

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
