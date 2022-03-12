#nullable disable

using System.Text.Json.Serialization;

namespace GitHubApiClient.Models.Responses
{
    public class IssueEvent
    {
        public long id { get; set; }
        public string node_id { get; set; }
        public string url { get; set; }
        public Actor actor { get; set; }
        [JsonPropertyName("event")]
        public string event_name { get; set; }
        public string commit_id { get; set; }
        public string commit_url { get; set; }
        public DateTime created_at { get; set; }
        public Issue issue { get; set; }
        public object performed_via_github_app { get; set; }
        public Rename? rename { get; set; }
        public UserInfo assignee { get; set; }
        public UserInfo assigner { get; set; }
        public MilestoneHeader milestone { get; set; }
    }

    public class Actor
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Rename
    {
        public string from { get; set; }
        public string to { get; set; }
    }

    public class MilestoneHeader
    {
        public string title { get; set; }
    }
}
