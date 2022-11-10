using Project;

namespace Project.Core;

public record ComFrequencyResultDTO(int CommitCount, DateTime CommitDate, int RepositoryId);
public record CreateComFrequencyResultDTO();

public record UpdateComFrequencyResultDTO(int Id, string RepoName, ICollection<int> Commits);