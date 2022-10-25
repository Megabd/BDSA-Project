using LibGit2Sharp;


// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, Mortals!");


using (var repo = new Repository("C:/Users/olive/ADS/BDSA-Project"))
 {
     ///get commits from all branches, not just master
     var commits = repo.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = repo.Refs });

     //here I can access commit's author, but not time
     var nameList = new List<String>();

     foreach (var Author in commits.Select(com => new Author(com.Author.Name, com.Author.When))){
        if(!nameList.Contains(Author.aName)){
            nameList.Add(Author.aName);
        }
     }

     foreach(var name in nameList){
        Console.WriteLine(name);
     }
     /*foreach (var name in nameList){
        if(commits.Select(com => new {Test = com.Author.Name} == name))
        commits.Select(com => new { Test = com.Author.Name, Date = com.Author.When});
     }*/
  }
public class Author {

    public string aName {get; set;}
    public DateTimeOffset aDate {get; set;}

    public Author(string name, DateTimeOffset date){
        aName = name;
        aDate = date;
    }
    }