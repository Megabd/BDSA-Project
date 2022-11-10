namespace Project.Core;

public interface IGitHubArchiveRepo {
     (Response Response, int GitHubArchiveId) Create(CreateGitHubArchiveDTO GitHubArch);
    
     GitHubArchiveDTO find(string GitHubArchName);

     Response Update(UpdateGitHubArchiveDTO GitHubArch);
}