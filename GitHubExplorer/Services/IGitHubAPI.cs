using System.Threading.Tasks;
using Refit;

namespace GitHubExplorer
{
    [Headers("Accept-Encoding: gzip",
                "Content-Type: application/json",
                "Authorization: bearer " + GitHubConstants.PersonalAccessToken,
                "User-Agent: GitHubExplorer")]
    interface IGitHubAPI
    {
        [Post("")]
        Task<GraphQLResponse<GitHubUserResponse>> UserQuery([Body] GraphQLRequest request);

        [Post("")]
        Task<GraphQLResponse<GitHubRepositoryResponse>> RepositoryQuery([Body] GraphQLRequest request);
    }
}
