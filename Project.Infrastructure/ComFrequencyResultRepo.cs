using Project.Core;
using Project;

namespace Project.Infrastructure;



public class ComFrequencyResultRepo : IComFrequencyResultRepo {

    private readonly ProjectContext _context;

    public ComFrequencyResultRepo (ProjectContext context){
        _context = context;
    }

public (Response Response, int ComFreResId) Create(CreateComFrequencyResultDTO UserRepo){
 
        Response response;


        var entity = new ComFrequencyResultRepo(UserRepo.repo){
            Name = UserRepo.RepoName
        };
           
        _context.Repositories.Add(entity);
        _context.SaveChanges();

        response = Response.Created;
        var commits = new List<int>();

        var created = new ComFrequencyResultDTO(entity.Id, entity.Name, commits);
        
        return (response, created.Id);

}


public IReadOnlyCollection<ComFrequencyResultDTO> GetComFrequencyResults(int RepositoryId){
 var repositories = from r in _context.FrequencyResults
                    where r.RepositoryId == RepositoryId
                    orderby r.CommitDate
                    select new ComFrequencyResultDTO(r.Id, r.Name, r.CommitList.Select(t =>t.Id).ToList<int>().AsReadOnly());

        if (repositories.Any()){
            return repositories.ToArray()!;
        }
        else {
            return null!;
        }           
}

public ComFrequencyResultDTO? Find(int UserRepoId) {
    
    var Repositories = from r in _context.Repositories
                            where r.Id == UserRepoId
                            select new ComFrequencyResultDTO(r.Id, r.Name, r.CommitList.Select(t =>t.Id).ToList<int>().AsReadOnly());

        return Repositories.FirstOrDefault();
}

public Response Update(UpdateComFrequencyResultDTO UserRepo){

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
            entity.Name = ComFrequencyResultDTO.RepoName;
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