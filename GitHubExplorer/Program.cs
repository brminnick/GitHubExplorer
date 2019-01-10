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

            await PrintUsers(userList).ConfigureAwait(false);
            await PrintRepositories(repositoryDictionary).ConfigureAwait(false);

            Console.ReadLine();
        }

        static async Task PrintUsers(IEnumerable<string> userList)
        {
            var getUserTaskList = userList.Select(GitHubGraphQLService.GetUser).ToList();

            while (getUserTaskList.Any())
            {
                var getUserTask = await Task.WhenAny(getUserTaskList).ConfigureAwait(false);

                var user = await getUserTask.ConfigureAwait(false);

                Console.WriteLine(user);

                getUserTaskList.Remove(getUserTask);
            }
        }

        static async Task PrintRepositories(Dictionary<string, string> repositoryDictionary)
        {
            var getRepositoryTaskList = repositoryDictionary.Select(x => GitHubGraphQLService.GetRepository(x.Value, x.Key)).ToList();

            while (getRepositoryTaskList.Any())
            {
                var finishedGetRepositoryTask = await Task.WhenAny(getRepositoryTaskList).ConfigureAwait(false);

                var user = await finishedGetRepositoryTask.ConfigureAwait(false);

                Console.WriteLine(user);

                getRepositoryTaskList.Remove(finishedGetRepositoryTask);
            }
        }
    }
}
