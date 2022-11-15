namespace Project.Core;

public interface IGitHubArchiveRepo {
     (Response Response, int GitHubArchiveId) Create(CreateGitHubArchiveDTO GitHubArch);
    
     GitHubArchiveDTO Find(string GitHubArchName);

     Response Update(UpdateGitHubArchiveDTO GitHubArch);
}