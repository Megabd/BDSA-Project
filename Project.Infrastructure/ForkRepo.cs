using Project.Core;
namespace Project.Infrastructure;


public class ForkRepo : IForkRepo
{
    private readonly ProjectContext _context;

    public ForkRepo (ProjectContext context){
        _context = context;
    }

    public (Response Response, int ForkId) Create(CreateForkDTO Fork)
    {
        Response response;

        var existingId = from f in _context.ForkResults
        where f.ForkName == Fork.ForkName && f.RepositoryId == Fork.RepositoryId
        select f.Id;
        
        if(existingId.Count() <= 0){

        
        var entity = new Fork(){
            ForkName = Fork.ForkName,
            RepositoryId  = Fork.RepositoryId,
        };
           
        _context.ForkResults.Add(entity);
        _context.SaveChanges();

        response = Response.Created;

        var created = new ForkDTO(entity.Id, entity.ForkName, entity.RepositoryId);
        
        return (response, created.Id);
        }
        else {
            response = Response.Conflict;
            int id = existingId.FirstOrDefault();
            return (response , id);
        }
    }

    public ForkDTO Find(int ForkId)
    {
       var Fork = from f in _context.ForkResults
                                where f.Id == ForkId
                                select new ForkDTO(f.Id, f.ForkName, f.RepositoryId);
        if (Fork.Any()){
                return Fork.FirstOrDefault()!;
            }
            else {
                return null;
            }  
    }

    public IReadOnlyCollection<ForkDTO> GetAllForks(int RepositoryId)
    {
       var Forks = from f in _context.ForkResults
                      where f.RepositoryId == RepositoryId
                      select new ForkDTO(f.Id, f.ForkName, f.RepositoryId);

        if (Forks.Any()){
            return Forks.ToArray()!;
        }
        else {
            return null!;
        }   
    }
}

