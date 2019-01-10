using System;
using System.Threading.Tasks;

namespace GitHubExplorer
{
    class MainClass
    {
        public static async Task Main(string[] args)
        {
            var user = await GitHubGraphQLService.GetUser("brminnick");
            var repo = await GitHubGraphQLService.GetRepository("brminnick", "AsyncAwaitBestPractices");
        }
    }
}
