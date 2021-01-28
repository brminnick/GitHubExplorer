using System;
using System.Text;

namespace GitHubExplorer
{
    public class GitHubIssue
    {
        public GitHubIssue(string title, string body, DateTimeOffset createdAt, DateTimeOffset? closedAt, string state)
        {
            Title = title;
            Body = body;
            CreatedAt = createdAt;
            ClosedAt = closedAt;
            State = state;
        }

        public string Title { get; }

        public string Body { get; }

        public DateTimeOffset CreatedAt { get; }

        public DateTimeOffset? ClosedAt { get; }

        public string State { get; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(Title)}: {Title}");
            stringBuilder.AppendLine($"{nameof(CreatedAt)}: {CreatedAt}");
            stringBuilder.AppendLine($"{nameof(ClosedAt)}: {ClosedAt}");
            stringBuilder.AppendLine($"{nameof(State)}: {State}");

            return stringBuilder.ToString();
        }
    }
}
