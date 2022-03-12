using Refit;
using System.Runtime.Serialization;

namespace GitHubApiClient.Models.Requests
{
    public enum VisiblityOptions
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "public")]
        Public,
        [EnumMember(Value = "private")]
        Private
    }

    public enum RepoSortOptions
    {
        [EnumMember(Value = "created")]
        Created,
        [EnumMember(Value = "updated")]
        Updated,
        [EnumMember(Value = "pushed")]
        Pushed,
        [EnumMember(Value = "full_name")]
        FullName
    }

    public class RepositoryQuery : BaseQuery
    {
        [AliasAs("visibility")]
        public VisiblityOptions? Visiblity { get; set; }
        [AliasAs("sort")]
        public RepoSortOptions? Sort { get; set; }
        [AliasAs("before")]
        [Query(Format = "yyyy-MM-dd")]
        public DateTime? Before { get; set; }
    }
}
