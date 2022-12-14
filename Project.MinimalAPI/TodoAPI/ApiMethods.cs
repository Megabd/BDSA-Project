using LibGit2Sharp;
using System;

public class ApiMethods {

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


}