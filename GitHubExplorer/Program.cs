using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace GitHubExplorer
{
    class MainClass
    {
        public static async Task Main(string[] args)
        {
            var username = GitHubConstants.GitHubRepoDictionary.Values.First();
            var repositoryName = GitHubConstants.GitHubRepoDictionary.Keys.First();

            var gitHubUser = await GitHubGraphQLService.GetUser(username).ConfigureAwait(false);
            Console.WriteLine(gitHubUser);

            var gitHubRepository = await GitHubGraphQLService.GetRepository(username, repositoryName).ConfigureAwait(false);
            Console.WriteLine(gitHubRepository);

            try
            {
                var count = 0;
                var cancellationTokenSournce = new CancellationTokenSource();
                await foreach (var issueList in GitHubGraphQLService.GetRepositoryIssues(username, repositoryName, cancellationTokenSournce.Token))
                {
                    foreach (var issue in issueList)
                        Console.WriteLine(issue);

                    if (++count > 5)
                        cancellationTokenSournce.Cancel();
                }
            }
            catch(OperationCanceledException)
            {
                Console.WriteLine("GetRepositories Cancelled");
            }

            Console.WriteLine("Completed. Press Any Key.");
            Console.ReadLine();
        }
    }
}
