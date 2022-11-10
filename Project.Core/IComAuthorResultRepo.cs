namespace Project.Core;

public interface IComAuthorResultRepo {
    
    (Response Response, int ComAutResId) Create(CreateComAuthorResultDTO ComAutRes);
    IReadOnlyCollection<ComAuthorResultDTO> GetComAuthorResults(int RepositoryID);
}