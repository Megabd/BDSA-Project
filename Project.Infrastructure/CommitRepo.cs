using Project.Core;

namespace Project.Infrastructure;



public class CommitRepo : ICommitRepo {

    private readonly ProjectContext _context;

public CommitRepo (ProjectContext context){
        _context = context;
}
public (Response Response, int CommitId) Create(CreateCommitDTO Commit){
        Response response;

        var entity = new Commit(Commit.AuthorName, DateTimeOffset.UtcNow);
           
        _context.Commits.Add(entity);
        _context.SaveChanges();

        response = Response.Created;

        var created = new CommitDTO(entity.Id, entity.Author, entity.aDate);
        
        return (response, created.Id);
}
public IReadOnlyCollection<CommitDTO> ReadAll() {

    var Commits = from c in _context.Commits
                    orderby c.Id
                    select new CommitDTO(c.Id, c.Author, c.aDate);

        if (Commits.Any()){
            return Commits.ToArray()!;
        }
        else {
            return null!;
        }        

}
public CommitDTO? Find(int CommitId) {

     var Commits = from c in _context.Commits
                        where c.Id == CommitId
                            select new CommitDTO(c.Id, c.Author, c.aDate);
        if(Commits.Any()){
        return Commits.FirstOrDefault()!;
        }
        else {
            return null;
        }

}

}
