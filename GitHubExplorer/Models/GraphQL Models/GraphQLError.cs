using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExplorer
{
    class GraphQLError
    {
        public GraphQLError(string message, GraphQLLocation[] locations) => (Message, Locations) = (message, locations);

        public string Message { get; }

        public GraphQLLocation[] Locations { get; }

        [JsonExtensionData]
        public Dictionary<string, JToken> AdditonalEntries { get; set; } = new();
    }

    class GraphQLLocation
    {
        public GraphQLLocation(long line, long column) => (Line, Column) = (line, column);

        public long Line { get; }
        public long Column { get; }
    }
}
