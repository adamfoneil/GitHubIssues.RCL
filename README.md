I'd like to be able to show the Issues list of private repositories so I can show customers what I'm working on without sharing the repo itself, or for that matter have to log into GitHub.

Of course, there are projects like [Octokit](https://github.com/octokit) for exactly this, but it does a bazillion things. I would prefer a really targeted client that uses [Refit](https://github.com/reactiveui/refit). I'm always wanting to practice more with Refit and build up a portfolio of examples.

Key parts of this repo:
- [CLI](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubApi.CLI) project. This is what I'm testing interactively with on the console.
- [ApiClient](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubApiClient) project. This is where the main work is, with the Refit [interface](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/Interfaces/IGitHubClient.cs) and the [client object](https://github.com/adamfoneil/GitHubIssues.RCL/blob/master/GitHubApiClient/GitHubClient.cs) itself that you would use.
- [Razor Class Library](https://github.com/adamfoneil/GitHubIssues.RCL/tree/master/GitHubIssues.RCL) project. I'm not sure how far this will get. I originally envisioned creating a component or two that I can use within another Blazor app. This may turn out to be more ambitious than I want to get into right now, and I may just be happy with the API client alone.
