using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitHubExplorer
{
    public class IssuesConnection
    {
        public IssuesConnection(List<GitHubIssue> issueList, PageInfo pageInfo) =>
            (IssueList, PageInfo) = (issueList, pageInfo);

        [JsonProperty("nodes")]
        public List<GitHubIssue> IssueList { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfo PageInfo { get; set; }
    }
}
