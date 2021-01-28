using System;
using System.Text;

namespace GitHubExplorer
{
    public class GitHubUser
    {
        public GitHubUser(string name, string company, DateTimeOffset createdAt, GitHubFollowers followers)
        {
            Name = name;
            Company = company;
            AccountCreationDate = createdAt;
            FollowerCount = followers.Count;
        }

        public string Name { get; }

        public string Company { get; }

        public DateTimeOffset AccountCreationDate { get; }

        public int FollowerCount { get; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(Name)}: {Name}");
            stringBuilder.AppendLine($"{nameof(Company)}: {Company}");
            stringBuilder.AppendLine($"{nameof(FollowerCount)}: {FollowerCount}");
            stringBuilder.AppendLine($"{nameof(AccountCreationDate)}: {AccountCreationDate}");

            return stringBuilder.ToString();
        }
    }

    public class GitHubFollowers
    {
        public GitHubFollowers(int count) => Count = count;

        public int Count { get; }
    }
}
