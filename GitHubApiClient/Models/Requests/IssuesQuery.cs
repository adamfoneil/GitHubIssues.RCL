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
        [EnumMember(Value = "created")]
        Created,
        [EnumMember(Value = "updated")]
        Updated,
        [EnumMember(Value = "comments")]
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
        [EnumMember(Value = "assigned")]
        AssignedToMe,
        [EnumMember(Value = "created")]
        CreatedByMe,
        [EnumMember(Value = "mentioned")]
        MentionedMe,
        [EnumMember(Value = "subscribed")]
        Subscribed,
        [EnumMember(Value = "all")]
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
        [AliasAs("direction")]
        public SortDirection? SortDirection { get; set; }
        [AliasAs("since")]
        [Query(Format = "yyyy-MM-dd")]
        public DateTime? Since { get; set; }
        [AliasAs("sort")]
        public IssueSort? SortBy { get; set; }
    }
}
