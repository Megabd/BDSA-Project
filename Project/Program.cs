using LibGit2Sharp;


// See https://aka.ms/new-console-template for more information

using (var repo = new Repository("C:/Users/Mathias/Desktop/ITU_mappe/3.rd_semester/Analysis, Design and Software Architecture/BDSA-Project"))
{
   ///get commits from all branches, not just master
   var commits = repo.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = repo.Refs });

   //here I can access commit's author, but not time
   var authorList = new List<Author>();

   foreach (var Author in commits.Select(com => new Author(com.Author.Name, com.Author.When)))
   {
      if(!authorList.Contains(Author))
      {
         authorList.Add(Author);
      }
   }
   Console.WriteLine("Press 1 for Commit fequency. Press 2 for Commit Author");
   string consoleInput = Console.ReadLine()!;
   if(consoleInput == "1")
   {
      CommitFequency();
   }
   if(consoleInput =="2")
   {
      CommitAuthor();
   }

   void CommitFequency()
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

   void CommitAuthor()
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