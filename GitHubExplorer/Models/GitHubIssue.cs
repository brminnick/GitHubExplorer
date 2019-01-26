using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace GitHubExplorer
{
    public class GitHubIssue
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("closedAt")]
        public DateTimeOffset? ClosedAt { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfo PageInfo { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(Title)}: {Title}");
            stringBuilder.AppendLine($"{nameof(CreatedAt)}: {CreatedAt}");
            stringBuilder.AppendLine($"{nameof(ClosedAt)}: {ClosedAt}");
            stringBuilder.AppendLine($"{nameof(State)}: {State}");
            stringBuilder.AppendLine($"{nameof(Body)}: {Body}");

            return stringBuilder.ToString();
        }
    }
}
