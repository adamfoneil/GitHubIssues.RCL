using Refit;

namespace GitHubApiClient.Models.Requests
{
    public class BaseQuery
    {
        [AliasAs("page")]
        public int Page { get; set; } = 1;
        [AliasAs("per_page")]
        public int PerPage { get; set; } = 30;
    }
}
