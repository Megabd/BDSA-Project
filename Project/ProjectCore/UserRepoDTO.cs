public record UserRepoDTO (int Id, string RepoName, IReadOnlyCollection<int> Commits);
public record CreateUserRepoDTO(string RepoName);

public record UpdateUserRepoDTO(int Id, string RepoName, ICollection<int> Commits);