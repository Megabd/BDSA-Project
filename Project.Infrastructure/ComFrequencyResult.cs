
namespace Project.Infrastructure;

public class ComFrequencyResult {
    int CommitCount {get; set;}
    DateTimeOffset CommitDate {get; set;}

    int RepositoryId {get; set;}

    public ComFrequencyResult(){
        
    }

}