namespace Project.Core;


public record ForkDTO(int Id, string ForkName, int RepositoryId);
public record CreateForkDTO(string ForkName, int RepositoryId);
