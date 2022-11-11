using Project.Core;
namespace Project.Infrastructure;
public class GitHubArchiveRepo : IGitHubArchiveRepo {

    private readonly ProjectContext _context;

public GitHubArchiveRepo (ProjectContext context){
        _context = context;
}

    (Response Response, int GitHubArchiveId) Create(CreateGitHubArchiveDTO GitHubArch){
        Response response;

        var entity = new GitHubArchive(); /*{
            //add values for everything here
        }*/
           
        _context.Repositories.Add(entity);
        _context.SaveChanges();

        response = Response.Created;

        var created = new GitHubArchiveDTO(entity.Id, entity.RepositoryName, entity.LatestCommit);
        
        return (response, created.Id);
    }
    
     GitHubArchiveDTO find(string GitHubArchName){
        var GitArch = from r in _context.Repositories
                  where r.RepositoryName == GitHubArchName
                  select new GitHubArchiveDTO(r.Id, r.RepositoryName, r.LatestCommit);
    if (GitArch.Any()){
            return GitArch.FirstOrDefault()!;
        }
        else {
            return null!;
        }    
     }

     Response Update(UpdateGitHubArchiveDTO GitHubArch){

     }

}