using System.Text;
using Newtonsoft.Json;

namespace GitHubExplorer
{
    public class GitHubRepository
    {
        public GitHubRepository() { }

        [JsonConstructor]
        public GitHubRepository(Owner owner, Stargazers stargazers) =>
            (RepositoryOwner, RepositoryStargazers) = (owner, stargazers);

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("forkCount")]
        public long ForkCount { get; set; }

        [JsonProperty("issues")]
        public IssuesConnection Issues { get; set; }

        public string Owner
        {
            get => RepositoryOwner?.Login;
            set
            {
                if (RepositoryOwner is null)
                    RepositoryOwner = new Owner { Login = value };
                else
                    RepositoryOwner.Login = value;
            }
        }

        public int Stargazers
        {
            get => RepositoryStargazers?.TotalCount ?? -1;
            set
            {
                if (RepositoryStargazers is null)
                    RepositoryStargazers = new Stargazers { TotalCount = value };
                else
                    RepositoryStargazers.TotalCount = value;
            }
        }

        [JsonProperty("owner")]
        Owner RepositoryOwner { get; set; }

        [JsonProperty("stargazers")]
        Stargazers RepositoryStargazers { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(Name)}: {Name}");
            stringBuilder.AppendLine($"{nameof(Owner)}: {Owner}");
            stringBuilder.AppendLine($"{nameof(Description)}: {Description}");
            stringBuilder.AppendLine($"{nameof(ForkCount)}: {ForkCount}");
            stringBuilder.AppendLine($"{nameof(Stargazers)}: {Stargazers}");

            return stringBuilder.ToString();
        }

    }

    public class Owner
    {
        [JsonProperty("login")]
        public string Login { get; set; }
    }

    public class Stargazers
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
