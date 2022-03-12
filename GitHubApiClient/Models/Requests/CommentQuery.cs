using Refit;

namespace GitHubApiClient.Models.Requests
{
    public class CommentQuery : BaseQuery
    {
        [AliasAs("sort")]
        public SortDirection? SortDirection { get; set; }
        [AliasAs("since")]
        [Query(Format = "yyyy-MM-dd")]
        public DateTime? Since { get; set; }
    }
}
