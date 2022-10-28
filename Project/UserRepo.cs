using LibGit2Sharp;

namespace Project;

public class UserRepo {

public List<Author> authorList {get; set;}
public UserRepo(string pathName){
   authorList = new List<Author>(); 
        using (var repo = new Repository(pathName)){
   ///get commits from all branches, not just master
   var commits = repo.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = repo.Refs });

   //here I can access commit's author, but not time

   foreach (var Author in commits.Select(com => new Author(com.Author.Name, com.Author.When)))
   {
      if(!authorList.Contains(Author))
      {
         authorList.Add(Author);
      }
   }
    }

}
public void CommitFequency(List<Author>authorList)
   {
      var listOfUniqueDates = (
      from author in authorList
      orderby author.aDate 
      group author by new {author.aDate.Date} into g
      select new {date = g.Key, count = g.Count()}  
   );

   foreach(var output in listOfUniqueDates)
   {
      Console.WriteLine(output.count + " " + output.date.Date.ToString("dd-MM-yyyy"));
   }

   }

  public void CommitAuthor(List<Author>authorList)
   {
      var authorNameList = (
         (from author in authorList
         select new {name = author.aName}).Distinct()
      );
      foreach(var distAuthor in authorNameList)
      {
         Console.WriteLine(distAuthor.name);

         var listOfAuthorsCommitHistorie = (
         from author in authorList
         where author.aName == distAuthor.name
         orderby author.aDate
         group author by new
         {
            author.aName,
            author.aDate.Date
         } into g
         select new{key = g.Key, count = g.Count()}
         );
         foreach(var comDate in listOfAuthorsCommitHistorie)
         {
            Console.WriteLine("      " + comDate.count + " " + comDate.key.Date.ToString("dd-MM-yyyy"));
         }
      }
   }
}
