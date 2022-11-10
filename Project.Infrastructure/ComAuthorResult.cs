namespace Project.Infrastructure;

public class ComAuthorResult {


    public int Id {get; set;}
    public string? Author {get; set;}
    public int CommitAmount {get; set;}
    public DateTime CommitDate {get; set;}
    public int RepositoryId {get; set;}

    public ComAuthorResult(){

    }

}