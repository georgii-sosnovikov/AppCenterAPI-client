using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void ShowBranches(List<Root> roots)
        {
            foreach (Root root in roots)
            {
                Console.WriteLine($"{root.Branch.Name} build " +
                    $"{root.LastBuild.Result} in {root.LastBuild.FinishTime.Subtract(root.LastBuild.StartTime).TotalSeconds} seconds." +
                    $"Link to build logs {root.LastBuild.UrlToBuildLogs}\n");
            }
        }

        static void ShowJustCreatedBuilds(List<JustCreatedBuild> builds)
        {
            foreach (JustCreatedBuild build in builds)
            {
                Console.WriteLine($"Created a new build [id: {build.Id}] for {build.SourceBranch} branch");
            }
        }

        static async Task<List<Root>> GetBranchesAsync(string uri)
        {
            List<Root> branches = null;
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                branches = await response.Content.ReadAsAsync<List<Root>>();
            }
            return branches;
        }

        static async Task<LinkToBuildLogs> GetLinkToBuildLogsAsync(string uri)
        {
            LinkToBuildLogs link = null;
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                link = await response.Content.ReadAsAsync<LinkToBuildLogs>();
            }
            return link;
        }

        static async Task<JustCreatedBuild> CreateBranchBuildAsync(string uri)
        {
            JustCreatedBuild build = null;
            CreatingBuildParams param = new CreatingBuildParams();
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, param);
            if (response.IsSuccessStatusCode)
            {
                build = await response.Content.ReadAsAsync<JustCreatedBuild>();
            }
            return build;
        }

        static async Task Main()
        {
            string token = "YOUR_TOKEN";
            client.BaseAddress = new Uri("https://api.appcenter.ms/v0.1/apps/OWNER_NAME/APP_NAME/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.DefaultRequestHeaders.Add("X-API-Token", token);

            List<Root> branches = null;
            try
            {
                Console.WriteLine("Retrieving data about all branches...");

                branches = await GetBranchesAsync("branches");

                if (branches != null)
                {
                    LinkToBuildLogs linkToBuildLogs = null;
                    foreach (Root branch in branches)
                    {
                        linkToBuildLogs = await GetLinkToBuildLogsAsync("builds/" + branch.LastBuild.Id + "/downloads/logs");
                        branch.LastBuild.UrlToBuildLogs = linkToBuildLogs.Uri;
                    }
                    ShowBranches(branches);

                    List<JustCreatedBuild> justCreatedBuilds = new List<JustCreatedBuild>();

                    Console.WriteLine("Creating new builds...");

                    foreach (Root branch in branches)
                    {
                        justCreatedBuilds.Add(await CreateBranchBuildAsync("branches/" + branch.Branch.Name + "/builds"));
                    }
                    ShowJustCreatedBuilds(justCreatedBuilds);
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
