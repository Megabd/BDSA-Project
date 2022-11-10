namespace Project.Infrastructure;

public class ComAutherResult {

    string Author {get; set;}
    int CommitAmount {get; set;}
    DateTimeOffset CommitDate {get; set;}
    int RepositoryId {get; set;}

    public ComAutherResult(){

    }

}