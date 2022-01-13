using GitHubApiClient.Models;
using Refit;

namespace GitHubApiClient.Interfaces
{
	[Headers("Authorization: Basic", "User-Agent: AO.GitHubApiClient")]
	internal interface IGitHubClient
	{
		[Get("/repos/{userName}/{repositoryName}/issues?page={page}")]
		Task<IReadOnlyCollection<Issue>> GetIssuesAsync(string userName, string repositoryName, int page = 1);
	}
}
