using GitHubApiClient.Models;
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
	}
}
