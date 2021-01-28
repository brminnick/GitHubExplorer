using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Refit;

namespace GitHubExplorer
{
    static class GitHubGraphQLService
    {
        readonly static Lazy<IGitHubAPI> _githubApiClientHolder = new Lazy<IGitHubAPI>(() => RestService.For<IGitHubAPI>(CreateHttpClient()));

        static IGitHubAPI GitHubApiClient => _githubApiClientHolder.Value;

        public static async IAsyncEnumerable<GitHubUser> GetUsers(IEnumerable<string> userList)
        {
            var getUserTaskList = userList.Select(GetUser).ToList();

            while (getUserTaskList.Any())
            {
                var finishedGetUserTask = await Task.WhenAny(getUserTaskList).ConfigureAwait(false);
                getUserTaskList.Remove(finishedGetUserTask);

                var user = await finishedGetUserTask.ConfigureAwait(false);

                yield return user;
            }
        }

        public static async IAsyncEnumerable<GitHubRepository> GetRepositories(Dictionary<string, string> repositoryDictionary)
        {
            var getRepositoryTaskList = repositoryDictionary.Select(x => GetRepository(x.Value, x.Key)).ToList();

            while (getRepositoryTaskList.Any())
            {
                var finishedGetRepositoryTask = await Task.WhenAny(getRepositoryTaskList).ConfigureAwait(false);
                getRepositoryTaskList.Remove(finishedGetRepositoryTask);

                var repository = await finishedGetRepositoryTask.ConfigureAwait(false);

                yield return repository;
            }
        }

        public static async Task<GitHubUser> GetUser(string username)
        {
            var requestString = "query { user(login: \"" + username + "\"){ name,company,createdAt, followers{ totalCount }}}";

            var data = await ExecuteGraphQLRequest(() => GitHubApiClient.UserQuery(new GraphQLRequest(requestString))).ConfigureAwait(false);

            return data.User;
        }

        public static async Task<GitHubRepository> GetRepository(string repositoryOwner, string repositoryName)
        {
            var requestString = "query { repository(owner:\"" + repositoryOwner + "\" name:\"" + repositoryName + "\"){ name, description, forkCount, owner { login }, stargazers { totalCount }}}";

            var data = await ExecuteGraphQLRequest(() => GitHubApiClient.RepositoryQuery(new GraphQLRequest(requestString))).ConfigureAwait(false);

            return data.Repository;
        }

        public static async IAsyncEnumerable<IReadOnlyList<GitHubIssue>> GetRepositoryIssues(string repositoryOwner, string repositoryName, [EnumeratorCancellation] CancellationToken cancellationToken, int numberOfIssuesPerRequest = 100)
        {
            IssuesConnection? issuesConnection = null;

            do
            {
                issuesConnection = await GetIssueConnection(repositoryOwner, repositoryName, numberOfIssuesPerRequest, issuesConnection?.PageInfo?.EndCursor).ConfigureAwait(false);

                if (issuesConnection?.IssueList is not null)
                    yield return issuesConnection.IssueList;

                cancellationToken.ThrowIfCancellationRequested();
            }
            while (issuesConnection?.PageInfo?.HasNextPage is true);
        }

        static async Task<IssuesConnection?> GetIssueConnection(string repositoryOwner, string repositoryName, int numberOfIssuesPerRequest, string? endCursor)
        {
            var endCursorString = string.IsNullOrWhiteSpace(endCursor) ? string.Empty : "after: \"" + endCursor + "\"";

            var requestString = "query { repository(owner:\"" + repositoryOwner + "\" name:\"" + repositoryName + "\"){ name, description, forkCount, owner { login }, stargazers { totalCount } issues(first:" + numberOfIssuesPerRequest + endCursorString + "){ nodes { title, body, createdAt, closedAt, state }, pageInfo { endCursor, hasNextPage, hasPreviousPage, startCursor }}}}";

            var data = await ExecuteGraphQLRequest(() => GitHubApiClient.RepositoryIssuesQuery(new GraphQLRequest(requestString))).ConfigureAwait(false);

            return data.Repository.Issues;
        }

        static async Task<T> ExecuteGraphQLRequest<T>(Func<Task<GraphQLResponse<T>>> action, int numRetries = 3)
        {
            var response = await Policy.Handle<Exception>().WaitAndRetryAsync(numRetries, pollyRetryAttempt).ExecuteAsync(action).ConfigureAwait(false);

            if (response.Errors != null)
                throw new AggregateException(response.Errors.Select(x => new Exception(x.Message)));

            return response.Data;

            static TimeSpan pollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }

        static HttpClient CreateHttpClient() => new(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.GZip })
        {
            BaseAddress = new Uri(GitHubConstants.APIUrl)
        };
    }
}
