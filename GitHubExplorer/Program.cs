using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubExplorer
{
    class MainClass
    {
        public static async Task Main(string[] args)
        {
            var userList = GitHubConstants.GitHubRepoDictionary.Values.Distinct();
            var repositoryDictionary = GitHubConstants.GitHubRepoDictionary;

            await foreach (var user in GetUsers(userList))
            {
                Console.WriteLine(user);
            }

            await foreach (var repository in GetRepositories(repositoryDictionary))
            {
                Console.WriteLine(repository);
            }

            Console.ReadLine();
        }

        static async IAsyncEnumerable<GitHubUser> GetUsers(IEnumerable<string> userList)
        {
            var getUserTaskList = userList.Select(GitHubGraphQLService.GetUser).ToList();

            while (getUserTaskList.Any())
            {
                var finishedGetUserTask = await Task.WhenAny(getUserTaskList).ConfigureAwait(false);
                getUserTaskList.Remove(finishedGetUserTask);

                var user = await finishedGetUserTask.ConfigureAwait(false);

                yield return user;
            }
        }

        static async IAsyncEnumerable<GitHubRepository> GetRepositories(Dictionary<string, string> repositoryDictionary)
        {
            var getRepositoryTaskList = repositoryDictionary.Select(x => GitHubGraphQLService.GetRepository(x.Value, x.Key)).ToList();

            while (getRepositoryTaskList.Any())
            {
                var finishedGetRepositoryTask = await Task.WhenAny(getRepositoryTaskList).ConfigureAwait(false);
                getRepositoryTaskList.Remove(finishedGetRepositoryTask);

                var repository = await finishedGetRepositoryTask.ConfigureAwait(false);

                yield return repository;
            }
        }
    }
}
