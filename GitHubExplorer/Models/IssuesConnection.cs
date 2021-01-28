using System.Collections.Generic;

namespace GitHubExplorer
{
    public class IssuesConnection
    {
        public IssuesConnection(GitHubIssue[] nodes, PageInfo pageInfo) =>
            (IssueList, PageInfo) = (nodes, pageInfo);

        public IReadOnlyList<GitHubIssue> IssueList { get; }

        public PageInfo PageInfo { get; }
    }
}
