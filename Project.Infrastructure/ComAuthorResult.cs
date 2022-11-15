namespace Project.Infrastructure;

public class ComAuthorResult {


    public int Id {get; set;}
    public string Author {get; set;} = null!;
    public int CommitCount {get; set;}
    public DateTime CommitDate {get; set;}
    public int RepositoryId {get; set;}



}