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

    public class IssuesQuery : BaseQuery
    {
        [AliasAs("filter")]
        public IssueFilter? Filter { get; set; }
        [AliasAs("state")]
        public IssueState? State { get; set; }
        [AliasAs("labels")]
        [Query(CollectionFormat = CollectionFormat.Csv)]
        public string[] Labels { get; set; }
    }
}
