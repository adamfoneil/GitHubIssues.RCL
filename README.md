I'd like to be able to show the Issues list of private repositories so I can show customers what I'm working on without sharing the repo itself, or for that matter have to log into GitHub.

Of course, there are projects like [Octokit](https://github.com/octokit) for exactly this, but it does a bazillion things. I would prefer a really targeted client that uses [Refit](https://github.com/reactiveui/refit). I'm always wanting to practice more with Refit and build up a portfolio of examples.

Key parts of this repo:
- [CLI](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubApi.CLI) project. This is what I'm testing interactively with on the console.
- [ApiClient](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubApiClient) project. This is where the main work is, with the Refit [interface](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Interfaces/IGitHubClient.cs) and the [client object](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs) itself that you would use. I'm using Postman to execute fetches against the GitHub API itself, and using responses to build my `Response` [Models](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubApiClient/Models/Responses).
- [Razor Class Library](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubIssues.RCL) project. I'm not sure how far this will get. I originally envisioned creating a component or two that I can use within another Blazor app. This may turn out to be more ambitious than I want to get into right now, and I may just be happy with the API client alone.

## GitHub Client Scope
For now, I care mainly about Issues and Issue Events. I did add support for getting Comments, but I'm not sure actually need it. There is also support for querying repository info, but that was merely so I'd have access to the [open_issues_count](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Responses/Repository.cs#L74), a metric I'm intersted in.

The client methods themselves follow this pattern:
- A `Get` method returns a single page of results always. You must manually increment any `Page` argument and call your method again to get additional pages of results.
- There are two kinds of `GetAll` methods. These are used to enumerate all pages of a collection automatically via [EnumAllPagesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L60). Because some collections may be quite large and inefficent to query completely, some of the `GetAll` methods have a `shouldContinue` delegate you can use to cause the `GetAll` to stop after some condition is met. For example, one query I want to do is to get 10 most recently closed issues. I can do that like this:

```csharp
var events = (await client.GetAllEventsAsync("MyRepo", 
    (qry, results) => results.Count(ev => ev.event_name.Equals("closed")) < 10))
    .Where(ev => ev.event_name.Equals("closed"))
    .Take(10);
```
  This says, "get events in 'MyRepo' while the total number of 'closed' events is less than 10. Then, from the all the returns events, give me the first 10 of those that are 'closed'.

## Reference
- Task\<IReadOnlyCollection\<Repository\>\> [GetMyRepositoriesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L36)
 ([ [RepositoryQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/RepositoryQuery.cs#L28) query ])
- Task\<IReadOnlyCollection\<Repository\>\> [GetAllMyRepositoriesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L39)
 ([ [RepositoryQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/RepositoryQuery.cs#L28) query ])
- Task\<IReadOnlyCollection\<Issue\>\> [GetIssuesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L42)
 (string repositoryName, [ [IssuesQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/IssuesQuery.cs#L50) query ])
- Task\<IReadOnlyCollection\<Issue\>\> [GetAllIssuesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L45)
 (string repositoryName, [ [IssuesQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/IssuesQuery.cs#L50) query ])
- Task\<IReadOnlyCollection\<Issue\>\> [GetAllIssuesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L48)
 (string repositoryName, Func<IssuesQuery, IEnumerable<Issue>, bool> shouldContine, [ [IssuesQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/IssuesQuery.cs#L50) query ])
- Task\<IReadOnlyCollection\<IssueEvent\>\> [GetEventsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L51)
 (string repositoryName, [ [BaseQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/BaseQuery.cs#L5) query ])
- Task\<IReadOnlyCollection\<IssueEvent\>\> [GetAllEventsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L54)
 (string repositoryName, Func<BaseQuery, IEnumerable<IssueEvent>, bool> shouldContinue, [ [BaseQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/BaseQuery.cs#L5) query ])
- Task\<IReadOnlyCollection\<Comment\>\> [GetCommentsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L57)
 (string repositoryName, [ [CommentQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/CommentQuery.cs#L5) query ])
- Task\<IReadOnlyCollection\<Comment\>\> [GetAllCommentsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L60)
 (string repositoryName, [ [CommentQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/CommentQuery.cs#L5) query ])
- Task\<IReadOnlyCollection\<Comment\>\> [GetAllCommentsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L63)
 (string repositoryName, Func<CommentQuery, IEnumerable<Comment>, bool> shouldContine, [ [CommentQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/CommentQuery.cs#L5) query ])
