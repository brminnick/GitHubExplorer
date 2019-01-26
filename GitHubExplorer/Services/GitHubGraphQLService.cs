using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;
using Polly;
using System.Threading;

namespace GitHubExplorer
{
    static class GitHubGraphQLService
    {
        #region Constant Fields
        readonly static Lazy<IGitHubAPI> _githubApiClientHolder = new Lazy<IGitHubAPI>(() => RestService.For<IGitHubAPI>(GitHubConstants.APIUrl));
        #endregion

        #region Properties
        static IGitHubAPI GitHubApiClient => _githubApiClientHolder.Value;
        #endregion

        #region Methods
        public static async Task<GitHubUser> GetUser(string username)
        {
            var requestString = "query { user(login:" + username + "){ name,company,createdAt, followers{ totalCount }}}";

            var data = await ExecuteGraphQLRequest(() => GitHubApiClient.UserQuery(new GraphQLRequest(requestString))).ConfigureAwait(false);

            return data.User;
        }

        public static async Task<GitHubRepository> GetRepository(string repositoryOwner, string repositoryName)
        {
            var requestString = "query { repository(owner:\"" + repositoryOwner + "\" name:\"" + repositoryName + "\"){ name, description, forkCount, owner { login }, stargazers { totalCount }}}";

            var data = await ExecuteGraphQLRequest(() => GitHubApiClient.RepositoryQuery(new GraphQLRequest(requestString))).ConfigureAwait(false);

            return data.Repository;
        }

        public static async IAsyncEnumerable<GitHubIssue> GetRepositoryIssues(string repositoryOwner, string repositoryName, int numberOfIssuesPerRequest = 100, CancellationToken cancellationToken = null)
        {
            IssuesConnection issuesConnection = null;

            do
            {
                issuesConnection = await GetIssueConnection(repositoryOwner, repositoryName, numberOfIssuesPerRequest, issuesConnection?.PageInfo?.EndCursor).ConfigureAwait(false);

                yield return issuesConnection.IssueList;
            }
            while (!cancellationToken.IsCancellationRequested && (issuesConnection?.PageInfo?.HasNextPage ?? false));
        }

        static async Task<IssuesConnection> GetIssueConnection(string repositoryOwner, string repositoryName, int numberOfIssuesPerRequest, string endCursor)
        {
            var requestString = "query { repository(owner:\"" + repositoryOwner + "\" name:\"" + repositoryName + "\"){ issues(first:" + numberOfIssuesToRequest + (string.IsNullOrWhiteSpace(endCursor) ? "" : $"after: {endCursor}" + "){ nodes { title, body, createdAt, closedAt, state }}}}";

            var data = await ExecuteGraphQLRequest(() => GitHubApiClient.RepositoryIssuesQuery(new GraphQLRequest(requestString))).ConfigureAwait(false);

            return data.Repository.Issues;
        }

        static async Task<T> ExecuteGraphQLRequest<T>(Func<Task<GraphQLResponse<T>>> action, int numRetries = 3)
        {
            var response = await Policy
                                .Handle<Exception>()
                                .WaitAndRetryAsync
                                (
                                    numRetries,
                                    pollyRetryAttempt
                                ).ExecuteAsync(action);


            if (response.Errors != null)
                throw new AggregateException(response.Errors.Select(x => new Exception(x.Message)));

            return response.Data;

            TimeSpan pollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }
        #endregion
    }
}
