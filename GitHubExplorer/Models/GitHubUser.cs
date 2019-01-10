using System;
using Newtonsoft.Json;

namespace GitHubExplorer
{
    public class GitHubUser
    {
        public GitHubUser() { }

        [JsonConstructor]
        public GitHubUser(GitHubFollowers followers) => Followers = followers;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset AccountCreationDate { get; set; }

        public int FollowerCount
        {
            get => Followers?.Count ?? -1;
            set
            {
                if (Followers is null)
                    Followers = new GitHubFollowers { Count = value };
                else
                    Followers.Count = value;
            }
        }

        [JsonProperty("followers")]
        GitHubFollowers Followers { get; set; }
    }

    public class GitHubFollowers
    {
        [JsonProperty("totalCount")]
        public int Count { get; set; }
    }
}
