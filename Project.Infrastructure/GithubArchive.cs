namespace Project.Infrastructure;

public class GitHubArchive {

    int id {get; set;}
    string RepositoryName {get; set;}

    DateTimeOffset LatestCommit {get; set;}

    public GitHubArchive(){
        
    }
}