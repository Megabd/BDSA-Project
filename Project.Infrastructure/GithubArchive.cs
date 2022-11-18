

namespace Project.Infrastructure;

public class GitHubArchive {

    public string RepositoryName {get; set;} = null!;

    public int Id {get; set;}

    public DateTimeOffset LatestCommit {get; set;}

}