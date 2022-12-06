namespace Project.Core;

public record GitHubArchiveDTO(int Id, string RepositoryName, DateTimeOffset LatestCommit);
public record CreateGitHubArchiveDTO(string RepositoryName, DateTimeOffset LatestCommit);
public record UpdateGitHubArchiveDTO(int Id, DateTimeOffset LatestCommit);