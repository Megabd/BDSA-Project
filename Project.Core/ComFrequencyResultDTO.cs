using Project;

namespace Project.Core;

public record ComFrequencyResultDTO(int CommitCount, DateTime CommitDate, int RepositoryId);
public record CreateFrequencyResultDTO();

public record UpdateFrequencyResultDTO(int Id, string RepoName, ICollection<int> Commits);