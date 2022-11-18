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
            LatestCommit  = RepositoryMethods.latestCommit(GitHubArch.RepositoryName),
            
        };
           
        _context.Repositories.Add(entity);
        _context.SaveChanges();

        response = Response.Created;

        var created = new GitHubArchiveDTO(entity.Id, entity.RepositoryName, entity.LatestCommit);
        
        return (response, created.Id);
        }
        else {
            response = Response.Conflict;
            int id = existingId.FirstOrDefault();
            return (response , id);
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
            return Response.Updated;
        }
         _context.SaveChanges();
        return Response.Updated;
     }

}