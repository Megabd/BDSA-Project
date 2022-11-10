
namespace Project.Core;

public record ComAuthorResultDTO(string AuthorName, int Commits, DateTime CommitDate, int RepositoryId);
public record CreateAuthorResultDTO(string AuthorName, int Commits, DateTime CommitDate, int RepositoryId);
public record UpdateComAuthorResultDTO(int Commits);