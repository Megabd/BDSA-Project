using Project.Core;

namespace Project.Infrastructure;



public class ComAuthorResultRepo : IComAuthorResultRepo {

    private readonly ProjectContext _context;

public ComAuthorResultRepo (ProjectContext context){
        _context = context;
}
public (Response Response, int ComAutResId) Create(CreateComAuthorResultDTO ComAutRes){
        Response response;

        var entity = new ComAuthorResult(); /*{
            //add values for everything here
        }*/
           
        _context.AuthorResults.Add(entity);
        _context.SaveChanges();

        response = Response.Created;

        var created = new ComAuthorResultDTO(entity.Id, entity.Author, entity.CommitCount, entity.CommitDate, entity.RepositoryId);
        
        return (response, created.Id);
}
public IReadOnlyCollection<ComAuthorResultDTO> GetComAuthorResults(int RepositoryID) {

    var Commits = from c in _context.AuthorResults
                    where c.RepositoryId == RepositoryID
                    orderby c.Author
                    select new ComAuthorResultDTO(c.Id, c.Author, c.CommitCount, c.CommitDate, c.RepositoryId);

        if (Commits.Any()){
            return Commits.ToArray()!;
        }
        else {
            return null!;
        }        

}

public ComAuthorResultDTO Find (int ComAuthResId){
    var AuthRes = from r in _context.AuthorResults
                  where r.Id == ComAuthResId
                  select new ComAuthorResultDTO(r.Id, r.Author, r.CommitCount, r.CommitDate, r.RepositoryId);
    if (AuthRes.Any()){
            return AuthRes.FirstOrDefault()!;
        }
        else {
            return null!;
        }              
}


public Response Update (UpdateComAuthorResultDTO ComAuthRes) {
    Response response;
    var AuthRes = _context.AuthorResults.Find(ComAuthRes.Id);

    if (AuthRes is null){
        response = Response.NotFound;
    }       

    else {
        AuthRes.CommitCount = ComAuthRes.CommitCount;
        _context.SaveChanges();
        response = Response.Updated;
    }       

    return response;

}

}
