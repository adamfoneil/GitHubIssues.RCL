I'd like to be able to show the Issues list of private repositories so I can show customers what I'm working on without sharing the repo itself, or for that matter have to log into GitHub.

Of course, there are projects like [Octokit](https://github.com/octokit) for exactly this, but it does a bazillion things. I would prefer a really targeted client that uses [Refit](https://github.com/reactiveui/refit). I'm always wanting to practice more with Refit and build up a portfolio of examples.

Key parts of this repo:
- [CLI](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubApi.CLI) project. This is what I'm testing interactively with on the console.
- [ApiClient](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubApiClient) project. This is where the main work is, with the Refit [interface](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Interfaces/IGitHubClient.cs) and the [client object](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs) itself that you would use. I'm using Postman to execute fetches against the GitHub API itself, and using responses to build my `Response` [Models](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubApiClient/Models/Responses).
- [Razor Class Library](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubIssues.RCL) project. I'm not sure how far this will get. I originally envisioned creating a component or two that I can use within another Blazor app. This may turn out to be more ambitious than I want to get into right now, and I may just be happy with the API client alone.

## Reference

### [GitHubClient.cs](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L9)
- Task\<IReadOnlyCollection\<Repository\>\> [GetMyRepositoriesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L30)
 ([ [RepositoryQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/RepositoryQuery.cs#L28) query ])
- Task\<IReadOnlyCollection\<Repository\>\> [GetAllMyRepositoriesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L33)
 ([ [RepositoryQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/RepositoryQuery.cs#L28) query ])
- Task\<IReadOnlyCollection\<Issue\>\> [GetIssuesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L36)
 (string repositoryName, [ [IssuesQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/IssuesQuery.cs#L41) query ])
- Task\<IReadOnlyCollection\<IssueEvent\>\> [GetEventsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L39)
 (string repositoryName, [ [BaseQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/BaseQuery.cs#L5) query ])
- Task\<IReadOnlyCollection\<IssueEvent\>\> [GetAllEventsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L42)
 (string repositoryName, Func<BaseQuery, IEnumerable<IssueEvent>, bool> shouldContinue, [ [BaseQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/BaseQuery.cs#L5) query ])
- Task\<IReadOnlyCollection\<Comment\>\> [GetCommentsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L45)
 (string repositoryName, [ [CommentQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/CommentQuery.cs#L5) query ])
- Task\<IReadOnlyCollection\<Issue\>\> [GetAllIssuesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L48)
 (string repositoryName, [ [IssuesQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/IssuesQuery.cs#L41) query ])
- Task\<IReadOnlyCollection\<Issue\>\> [GetAllIssuesAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L51)
 (string repositoryName, Func<IssuesQuery, IEnumerable<Issue>, bool> shouldContine, [ [IssuesQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/IssuesQuery.cs#L41) query ])
- Task\<IReadOnlyCollection\<Comment\>\> [GetAllCommentsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L54)
 (string repositoryName, [ [CommentQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/CommentQuery.cs#L5) query ])
- Task\<IReadOnlyCollection\<Comment\>\> [GetAllCommentsAsync](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs#L57)
 (string repositoryName, Func<CommentQuery, IEnumerable<Comment>, bool> shouldContine, [ [CommentQuery?](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Models/Requests/CommentQuery.cs#L5) query ])
