using LibGit2Sharp;
using System.Net.Http;
using System;

public class ApiMethods {

    static readonly HttpClient client = new HttpClient();

    public static string CloneRepo(string repoOwner, string repoName) {
        var url = "https://github.com/"+repoOwner+"/"+repoName;
        var path = @"..\temp\"+repoOwner+@"\"+repoName;
        var totalPath = path+@"\.git";
        if (!Directory.Exists(path)) {
            Repository.Clone(url, path, new CloneOptions());
        } else {
            var repo = new Repository(totalPath);
            var pullOptions = new PullOptions() {
                MergeOptions = new MergeOptions()
                {
                    FastForwardStrategy = FastForwardStrategy.Default
                }
            };
            var signature = new Signature("_", "_", DateTimeOffset.Now);
            Commands.Pull(repo, signature, pullOptions);
        }
        return totalPath;
    }

    public static async Task<string> GetForks(string repo) {
        try {
            var requestContent = new FormUrlEncodedContent(new [] {
                new KeyValuePair<string, string>("-H", "Accept: application/vnd.github+json"),
                new KeyValuePair<string, string>("-H", "Authorization: Bearer "+"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"),
            });
            
            var requestString = "https://github.com/repos/"+repo+"forks";

            using HttpResponseMessage response = await client.PostAsync(requestString, requestContent);
            return response.Content.ReadAsStringAsync().Result;
        }
        catch (HttpRequestException e) {
            Console.WriteLine("Error: " + e.Message);
            return "";
        }
    }


}