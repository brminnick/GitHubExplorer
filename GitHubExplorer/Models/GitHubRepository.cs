using System.Text;

namespace GitHubExplorer
{
    public class GitHubRepository
    {
        public GitHubRepository(string name, string description, long forkCount, IssuesConnection? issues, Owner owner, Stargazers stargazers)
        {
            Name = name;
            Description = description;
            ForkCount = forkCount;
            Owner = owner.Login;
            Issues = issues;
            Stargazers = stargazers.TotalCount;
        }

        public string Owner { get; }

        public int? Stargazers { get; }

        public string Name { get; }

        public string Description { get; }

        public long ForkCount { get; }

        public IssuesConnection? Issues { get; }

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
        public Owner(string login) => Login = login;

        public string Login { get; }
    }

    public class Stargazers
    {
        public Stargazers(int totalCount) => TotalCount = totalCount;

        public int TotalCount { get; }
    }
}
