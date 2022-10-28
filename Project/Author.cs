namespace Project;
public class Author 
{
    public string aName {get; set;}
    public DateTimeOffset aDate {get; set;}
    public Author(string name, DateTimeOffset date)
    {
      aName = name;
      aDate = date;
    }
}