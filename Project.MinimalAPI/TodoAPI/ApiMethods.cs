using LibGit2Sharp;
using System;

public class ApiMethods {

    public static string CloneRepo(string repoOwner, string repoName) {
        var url = "https://github.com/"+repoOwner+"/"+repoName;
        var path = @"..\temp\"+repoOwner+@"\"+repoName;
        if (!Directory.Exists(path)) {
            var repo = Repository.Clone(url, path, new CloneOptions());
        }
        var totalPath = path+@"\.git";
        return totalPath;
    }


}