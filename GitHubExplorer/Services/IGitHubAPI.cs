using System.Threading.Tasks;
using Refit;

namespace GitHubExplorer
{
    [Headers("Accept-Encoding: gzip",
                "Accept: application/json")]
    interface IGitHubAPI
    {
        [Post("")]
        Task<GraphQLResponse<TeamsQueryDataResponse>> UserQuery([Body] GraphQLRequest request);

        [Post("")]
        Task<GraphQLResponse<IncrementPointsDataResponse>> IncrementPoints([Body] GraphQLRequest request);
    }
}
