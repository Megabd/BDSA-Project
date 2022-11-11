
namespace Project.Core;

public record ComAuthorResultDTO(int Id, string AuthorName, int CommitCount, DateTime CommitDate, int RepositoryId);
public record CreateComAuthorResultDTO(int RepositoryId);
public record UpdateComAuthorResultDTO(int Id, int CommitCount);