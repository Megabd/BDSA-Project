

namespace Project.Infrastructure;

public class GitHubArchive {

    public int Id {get; set;}
    public string RepositoryName {get; set;} = null!;

    public DateTimeOffset LatestCommit {get; set;}

}