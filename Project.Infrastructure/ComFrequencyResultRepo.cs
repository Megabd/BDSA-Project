using Project.Core;
using Project;

namespace Project.Infrastructure;



public class ComFrequencyResultRepo : IComFrequencyResultRepo {

    private readonly ProjectContext _context;

    public ComFrequencyResultRepo (ProjectContext context){
        _context = context;
    }

public (Response Response, int ComFreResId) Create(CreateComFrequencyResultDTO ComFreRes){
 
    Response response;

    var entity = new ComFrequencyResult(){
        CommitCount = ComFreRes.CommitCount,
        CommitDate = ComFreRes.CommitDate,
        RepositoryId = ComFreRes.RepositoryId
    };
        
    _context.FrequencyResults.Add(entity);
    _context.SaveChanges();

    response = Response.Created;

    var created = new ComFrequencyResultDTO(entity.Id, entity.CommitCount, entity.CommitDate, entity.RepositoryId);
    
    return (response, created.Id);
}


public IReadOnlyCollection<ComFrequencyResultDTO> GetComFrequencyResults(int RepositoryId){
 var ComFreqResults = from r in _context.FrequencyResults
                    where r.RepositoryId == RepositoryId
                    orderby r.CommitDate
                    select new ComFrequencyResultDTO(r.Id, r.CommitCount, r.CommitDate, r.RepositoryId);

        if (ComFreqResults.Any()){
            return ComFreqResults.ToArray()!;
        }
        else {
            return null!;
        }           
}

public ComFrequencyResultDTO? Find(int ComFreqResultId) {
    
    var ComFreq = from r in _context.FrequencyResults
                            where r.Id == ComFreqResultId
                            select new ComFrequencyResultDTO(r.Id, r.CommitCount, r.CommitDate, r.RepositoryId);
    if (ComFreq.Any()){
            return ComFreq.FirstOrDefault()!;
        }
        else {
            return null;
        }  
}

public Response Update(UpdateComFrequencyResultDTO ComFreqRes){

    var entity = _context.FrequencyResults.Find(ComFreqRes.Id);
        Response response;

        if (entity is null)
        {
            response = Response.NotFound;
        }
        else if(entity.CommitCount < ComFreqRes.CommitCount)
        {
            entity.CommitCount = ComFreqRes.CommitCount;
            _context.SaveChanges();
            response = Response.Updated;
        }
        else {
            //already up to date
            response = Response.Updated;
        }

        return response;

}

}