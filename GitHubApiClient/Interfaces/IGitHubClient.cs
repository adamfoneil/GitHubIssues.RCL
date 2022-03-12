using GitHubApiClient.Models.Requests;
using GitHubApiClient.Models.Responses;
using Refit;

namespace GitHubApiClient.Interfaces
{
    [Headers("Authorization: Basic", "User-Agent: AO.GitHubApiClient")]
    internal interface IGitHubClient
    {
        [Get("/repos/{userName}/{repositoryName}/issues")]
        Task<IReadOnlyCollection<Issue>> GetIssuesAsync(string userName, string repositoryName, IssuesQuery? query = null);

        [Get("/repos/{userName}/{repositoryName}/issues/comments")]
        Task<IReadOnlyCollection<Comment>> GetCommentsAsync(string userName, string repositoryName, CommentQuery? query = null);

        [Get("/user/repos")]
        Task<IReadOnlyCollection<Repository>> GetMyRepositoriesAsync(RepositoryQuery? query = null);

        [Get("/repos/{userName}/{repositoryName}/issues/events")]
        Task<IReadOnlyCollection<IssueEvent>> GetIssueEventsAsync(string userName, string repositoryName, BaseQuery? query = null);
    }
}
