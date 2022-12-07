using LibGit2Sharp;
using System.Net.Http;
using System.Net;
using System;
using Octokit;

public class ApiMethods {

    public static string CloneRepo(string repoOwner, string repoName) {
        var url = "https://github.com/"+repoOwner+"/"+repoName;
        var path = @"..\temp\"+repoOwner+@"\"+repoName;
        var totalPath = path+@"\.git";
        if (!Directory.Exists(path)) {
            LibGit2Sharp.Repository.Clone(url, path, new CloneOptions());
        } else {
            var repo = new LibGit2Sharp.Repository(totalPath);
            var pullOptions = new PullOptions() {
                MergeOptions = new MergeOptions()
                {
                    FastForwardStrategy = FastForwardStrategy.Default
                }
            };
            var signature = new LibGit2Sharp.Signature("_", "_", DateTimeOffset.Now);
            Commands.Pull(repo, signature, pullOptions);
        }
        return totalPath;
    }
}