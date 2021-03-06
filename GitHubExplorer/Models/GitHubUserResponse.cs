﻿using Newtonsoft.Json;

namespace GitHubExplorer
{
    public class GitHubUserResponse
    {
        public GitHubUserResponse(GitHubUser user) => User = user;

        [JsonProperty("user")]
        public GitHubUser User { get; }
    }
}
