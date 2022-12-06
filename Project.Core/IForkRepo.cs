 namespace Project.Core;


public interface IForkRepo{
 
 
    (Response Response, int ForkId) Create(CreateForkDTO Fork);
    IReadOnlyCollection<ForkDTO> GetAllForks(int RepositoryID);
    
    public ForkDTO Find (int ForkId);

}