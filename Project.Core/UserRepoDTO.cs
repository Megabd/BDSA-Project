using Project;

namespace Project.Core;

public record UserRepoDTO(int Id, string RepoName, IReadOnlyCollection<int> Commits);
//public record CreateUserRepoDTO(string RepoName, Repository repo);

public record UpdateUserRepoDTO(int Id, string RepoName, ICollection<int> Commits);