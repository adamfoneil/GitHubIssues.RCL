using GitHubApiClient.Interfaces;
using GitHubApiClient.Models;
using GitHubApiClient.Models.Requests;
using GitHubApiClient.Models.Responses;
using Microsoft.Extensions.Options;
using Refit;
using System.Text;

namespace GitHubApiClient
{
    public class GitHubClient
    {
        private readonly string _userName;
        private readonly string _token;
        private readonly IGitHubClient _api;

        public GitHubClient(IOptions<Settings> settings) : this(settings.Value.UserName, settings.Value.Token)
        {
        }
        
        public GitHubClient(string userName, string token)
        {
            _userName = userName;
            _token = token;
            _api = RestService.For<IGitHubClient>("https://api.github.com", new RefitSettings()
            {
                AuthorizationHeaderValueGetter = async () =>
                {
                    var bytes = Encoding.UTF8.GetBytes($"{_userName}:{_token}");
                    var encoded = Convert.ToBase64String(bytes);
                    return await Task.FromResult(encoded);
                }
            });
        }

        public async Task<IReadOnlyCollection<Repository>> GetMyRepositoriesAsync(RepositoryQuery? query = null) =>
            await _api.GetMyRepositoriesAsync(query);

        public async Task<IReadOnlyCollection<Repository>> GetAllMyRepositoriesAsync(RepositoryQuery? query = null) =>
            await EnumAllPagesAsync<Repository, RepositoryQuery>(async (qry) => await _api.GetMyRepositoriesAsync(qry), (qry, results) => true, query ?? new RepositoryQuery());

        public async Task<IReadOnlyCollection<Issue>> GetIssuesAsync(string repositoryName, IssuesQuery? query = null) => 
            await _api.GetIssuesAsync(_userName, repositoryName, query);

        public async Task<IReadOnlyCollection<Issue>> GetAllIssuesAsync(string repositoryName, IssuesQuery? query = null) =>
            await GetAllIssuesAsync(repositoryName, (query, results) => true, query);

        public async Task<IReadOnlyCollection<Issue>> GetAllIssuesAsync(string repositoryName, Func<IssuesQuery, IEnumerable<Issue>, bool> shouldContine, IssuesQuery? query = null) =>
            await EnumAllPagesAsync(async (qry) => await _api.GetIssuesAsync(_userName, repositoryName, qry), shouldContine, query ?? new IssuesQuery());

        public async Task<IReadOnlyCollection<IssueEvent>> GetEventsAsync(string repositoryName, BaseQuery? query = null) =>
            await _api.GetIssueEventsAsync(_userName, repositoryName, query);

        public async Task<IReadOnlyCollection<IssueEvent>> GetAllEventsAsync(string repositoryName, Func<BaseQuery, IEnumerable<IssueEvent>, bool> shouldContinue, BaseQuery? query = null) =>
            await EnumAllPagesAsync(async (qry) => await _api.GetIssueEventsAsync(_userName, repositoryName, qry), shouldContinue, query ?? new BaseQuery());

        public async Task<IReadOnlyCollection<Comment>> GetCommentsAsync(string repositoryName, CommentQuery? query = null) => 
            await _api.GetCommentsAsync(_userName, repositoryName, query);

        public async Task<IReadOnlyCollection<Comment>> GetAllCommentsAsync(string repositoryName, CommentQuery? query = null) =>
            await GetAllCommentsAsync(repositoryName, (query, results) => true, query);

        public async Task<IReadOnlyCollection<Comment>> GetAllCommentsAsync(string repositoryName, Func<CommentQuery, IEnumerable<Comment>, bool> shouldContine, CommentQuery? query = null) =>
            await EnumAllPagesAsync(async (qry) => await _api.GetCommentsAsync(_userName, repositoryName, qry), shouldContine, query ?? new CommentQuery());
        
        private async Task<IReadOnlyCollection<TResult>> EnumAllPagesAsync<TResult, TQuery>(Func<TQuery, Task<IEnumerable<TResult>>> fetch, Func<TQuery, IEnumerable<TResult>, bool> shouldContine, TQuery query) where TQuery : BaseQuery, new()
        {
            if (query is null) query = new TQuery();

            List<TResult> results = new();
            
            query.Page = 1;
            do
            {
                var page = await fetch.Invoke(query);
                if (!page.Any()) break;
                results.AddRange(page);
                query.Page++;
            } while (shouldContine.Invoke(query, results));

            return results;
        }
    }
}