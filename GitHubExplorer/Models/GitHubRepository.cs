using Newtonsoft.Json;

namespace GitHubExplorer
{
    public class GitHubRepository
    {
        public GitHubRepository() { }

        [JsonConstructor]
        public GitHubRepository(Owner owner) => RepositoryOwner = owner;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("forkCount")]
        public long ForkCount { get; set; }

        [JsonProperty("issues")]
        public Issues Issues { get; set; }

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

        [JsonProperty("owner")]
        Owner RepositoryOwner { get; set; }

    }

    public class Owner
    {
        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
