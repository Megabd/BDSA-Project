namespace Project.Infrastructure;
public class Commit 
{

    public int Id {get; set;}
    public string Author {get; set;}
    public DateTimeOffset aDate {get; set;}
    public Commit(string name, DateTimeOffset date)
    {
      Author = name;
      aDate = date;
    }
}