namespace Project.Core;

public interface IGitHubArchiveRepo {
     (Response Response, int GitHubArchiveId) Create(CreateGitHubArchiveDTO GitHubArch);
    
     GitHubArchiveDTO Find(int GitHubArchId);

     Response Update(UpdateGitHubArchiveDTO GitHubArch);
}