using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;
using Polly;

namespace GitHubExplorer
{
    static class GitHubGraphQLService
    {
        #region Constant Fields
        readonly static Lazy<IGitHubAPI >_graphQLApiClientHolder = new Lazy<IGitHubAPI>(()=> RestService.For<IGitHubAPI>("https://developer.github.com/v4/"));
        #endregion

        #region Properties
        static IGitHubAPI GraphQLApiClient => _graphQLApiClientHolder.Value;
        #endregion

        #region Methods
        public static async Task<List<TeamScore>> GetTeamScoreList()
        {
            const string requestString = "query{teams{name, points}}";

            var request = await ExecutePollyFunction(() => GraphQLApiClient.TeamsQuery(new GraphQLRequest(requestString))).ConfigureAwait(false);

            if (request.Errors != null)
                throw new AggregateException(request.Errors.Select(x => new Exception(x.Message)));

            return request.Data.Teams;
        }

        public static async Task<TeamScore> VoteForTeamAndGetCurrentScore(TeamColor teamType)
        {
            var request = "mutation {incrementPoints(id:" + (int)teamType + ") {name, points}}";

            var response = await ExecutePollyFunction(() => GraphQLApiClient.IncrementPoints(new GraphQLRequest(request))).ConfigureAwait(false);

            if (response.Errors != null)
                throw new AggregateException(response.Errors.Select(x => new Exception(x.Message)));

            return response.Data.TeamScore;
        }

        public static Task VoteForTeam(TeamColor teamType) => VoteForTeamAndGetCurrentScore(teamType);

        static Task<T> ExecutePollyFunction<T>(Func<Task<T>> action, int numRetries = 3)
        {
            return Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync
                    (
                        numRetries,
                        pollyRetryAttempt
                    ).ExecuteAsync(action);

            TimeSpan pollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }
        #endregion
    }
}
