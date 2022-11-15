namespace Project.Core;

public record GitHubArchiveDTO(int Id, string RepositoryName, DateTimeOffset LatestCommit);
public record CreateGitHubArchiveDTO(string RepositoryName);
public record UpdateGitHubArchiveDTO(string RepositoryName, DateTimeOffset LatestCommit);