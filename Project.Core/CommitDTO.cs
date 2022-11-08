
namespace Project.Core;

public record CommitDTO(int Id, string AuthorName, DateTimeOffset commitDate);
public record CreateCommitDTO(string AuthorName);