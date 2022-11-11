using Project;

namespace Project.Core;

public record ComFrequencyResultDTO(int Id, int CommitCount, DateTime CommitDate, int RepositoryId);
public record CreateComFrequencyResultDTO(int RepositoryId);

public record UpdateComFrequencyResultDTO(int Id, int CommitCount);