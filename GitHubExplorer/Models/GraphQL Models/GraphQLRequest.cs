using Newtonsoft.Json;

namespace GitHubExplorer
{
    class GraphQLRequest
    {
        public GraphQLRequest(string query, string? variables = null) =>
            (Query, Variables) = (query, variables ?? string.Empty);

        [JsonProperty("query")]
        public string Query { get; }

        [JsonProperty("variables")]
        public string Variables { get; }
    }
}
