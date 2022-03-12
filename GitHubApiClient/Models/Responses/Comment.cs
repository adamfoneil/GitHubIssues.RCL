#nullable disable

using System.Text.Json.Serialization;

namespace GitHubApiClient.Models.Responses
{
    public class Comment
    {
        public string url { get; set; }
        public string html_url { get; set; }
        public string issue_url { get; set; }
        public long id { get; set; }
        public string node_id { get; set; }
        public UserInfo user { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string author_association { get; set; }
        public string body { get; set; }
        public CommentReactions reactions { get; set; }
        public object performed_via_github_app { get; set; }

        public int IssueId => Convert.ToInt32(issue_url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last());
    }

    public class CommentReactions
    {
        public string url { get; set; }
        public int total_count { get; set; }
        [JsonPropertyName("+1")]
        public int plus1 { get; set; }
        [JsonPropertyName("-1")]
        public int minus1 { get; set; }
        public int laugh { get; set; }
        public int hooray { get; set; }
        public int confused { get; set; }
        public int heart { get; set; }
        public int rocket { get; set; }
        public int eyes { get; set; }
    }
}
