using Project.Core;
using Project;
namespace Project.Infrastructure;
public class GitHubArchiveRepo : IGitHubArchiveRepo 
{

    private readonly ProjectContext _context;

public GitHubArchiveRepo (ProjectContext context){
        _context = context;
       
}

    public (Response Response, int GitHubArchiveId) Create(CreateGitHubArchiveDTO GitHubArch){
        Response response;

        var existingId = from r in _context.Repositories
        where r.RepositoryName == GitHubArch.RepositoryName
        select r.Id;
        
        if(existingId.Count() <= 0){

        
        var entity = new GitHubArchive(){
            RepositoryName = GitHubArch.RepositoryName,
            LatestCommit  = GitHubArch.LatestCommit,
            
        };
        _context.Repositories.Add(entity);
        _context.SaveChanges();

        response = Response.Created;

        var created = new GitHubArchiveDTO(entity.Id, entity.RepositoryName, entity.LatestCommit);
        
        return (response, created.Id);
        }
        else {
            int id = existingId.FirstOrDefault();
            var existingRepo = Find(id);
            if (existingRepo.LatestCommit == GitHubArch.LatestCommit){
                response = Response.Fetched;
                return (response , id);
            } 
            else {
                Update(new UpdateGitHubArchiveDTO(id, GitHubArch.LatestCommit));
                response = Response.Updated;
                return (response , id);
            }
            
        }
    }
    
    public  GitHubArchiveDTO? Find(int GitHubArchId){
        var GitArch = from r in _context.Repositories
                  where r.Id == GitHubArchId
                  select new GitHubArchiveDTO(r.Id, r.RepositoryName, r.LatestCommit);
    if (GitArch.Any()){
            return GitArch.FirstOrDefault()!;
        }
        else {
            return null;
        }    
     }

    public Response Update(UpdateGitHubArchiveDTO GitHubArch){

        var entity = _context.Repositories.Find(GitHubArch.Id);
        if (entity == null){
            return Response.NotFound;
        }
        else if (entity.LatestCommit < GitHubArch.LatestCommit){

            entity.LatestCommit = GitHubArch.LatestCommit;
             _context.SaveChanges();
            return Response.Updated;
        }
         _context.SaveChanges();
        return Response.Updated;
     }

    public Response UpdateForkAmount(UpdateGIthubArchiveForksDTO GitHubArch) {
        var entity = _context.Repositories.Find(GitHubArch.Id);
        if (entity == null){
            return Response.NotFound;
        }
        else {
            entity.ForkAmount = GitHubArch.ForkAmount;
             _context.SaveChanges();
            return Response.Updated;
        }
    }

}