using Project.Core;
using Project;

namespace Project.Infrastructure;



public class UserRepoRepo : IUserRepoRepo {

    private readonly ProjectContext _context;

    public UserRepoRepo (ProjectContext context){
        _context = context;
    }

/*public (Response Response, int UserRepoId) Create(CreateUserRepoDTO UserRepo){
 
        Response response;


        var entity = new UserRepo(UserRepo.repo){
            Name = UserRepo.RepoName
        };
           
        _context.Repositories.Add(entity);
        _context.SaveChanges();

        response = Response.Created;
        var commits = new List<int>();

        var created = new UserRepoDTO(entity.Id, entity.Name, commits);
        
        return (response, created.Id);

}*/


public IReadOnlyCollection<UserRepoDTO> ReadAll(){
 var repositories = from r in _context.Repositories
                    orderby r.Name
                    select new UserRepoDTO(r.Id, r.Name, r.CommitList.Select(t =>t.Id).ToList<int>().AsReadOnly());

        if (repositories.Any()){
            return repositories.ToArray()!;
        }
        else {
            return null!;
        }           
}

public UserRepoDTO? Find(int UserRepoId) {
    
    var Repositories = from r in _context.Repositories
                            where r.Id == UserRepoId
                            select new UserRepoDTO(r.Id, r.Name, r.CommitList.Select(t =>t.Id).ToList<int>().AsReadOnly());

        return Repositories.FirstOrDefault();
}

public Response Update(UpdateUserRepoDTO UserRepo){

    var entity = _context.Repositories.Find(UserRepo.Id);
        Response response;

        if (entity is null)
        {
            response = Response.NotFound;
        }
        else if (_context.Repositories.FirstOrDefault(r => r.Id != UserRepo.Id && r.Id == UserRepo.Id) != null)
        {
            response = Response.Conflict;
        }
        else
        {
            entity.Name = UserRepo.RepoName;
            var comList = new List<Commit>();
            foreach (int i in UserRepo.Commits){
                if((from c in _context.Commits where c.Id== i select c).FirstOrDefault()!= null){
                    comList.Add((from c in _context.Commits where c.Id== i select c).FirstOrDefault()!);
                }
            }
            entity.CommitList = comList;
            _context.SaveChanges();
            response = Response.Updated;
        }

        return response;

}

}