#nullable disable

using Refit;
using System.Runtime.Serialization;

namespace GitHubApiClient.Models.Requests
{
    public enum IssueState
    {
        [EnumMember(Value = "open")]
        Open,
        [EnumMember(Value = "closed")]
        Closed,
        [EnumMember(Value = "all")]
        All
    }

    public enum IssueSort
    {
        Created,
        Updated,
        Comments
    }

    public enum SortDirection
    {
        [EnumMember(Value = "asc")]
        Ascending,
        [EnumMember(Value = "desc")]
        Descending
    }

    public enum IssueFilter
    {
        Assigned,
        Created,
        Mentioned,
        All
    }

    public class IssuesQuery
    {
        [AliasAs("filter")]
        public IssueFilter? Filter { get; set; }
        [AliasAs("state")]
        public IssueState? State { get; set; }
        [AliasAs("sort")]
        public SortDirection? SortDirection { get; set; }
        [AliasAs("labels")]
        [Query(CollectionFormat = CollectionFormat.Csv)]
        public string[] Labels { get; set; }
        [AliasAs("since")]
        [Query(Format = "yyyy-MM-dd")]
        public DateTime? Since { get; set; }
        public int Page { get; set; } = 1;
    }
}
