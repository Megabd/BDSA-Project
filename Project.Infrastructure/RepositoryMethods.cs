using LibGit2Sharp;
namespace Project.Infrastructure;


public class RepositoryMethods {
public static void addCommitsToRepository(string pathName) {
    List<Commit> CommitList = new List<Commit>();
        using (var repository = new Repository(pathName)){
        ///get commits from all branches, not just master
        var commits = repository.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = repository.Refs });
    
         /*this.Name = pathName.Substring(pathName.LastIndexOf('/') + 1);
         Console.WriteLine(Name);*/
            //here I can access commit's author, but not time
            foreach (var Author in commits.Select(com => new Commit(com.Author.Name, com.Author.When)))
            {
                if (!CommitList.Contains(Author))
                {
                    CommitList.Add(Author);
                }
            }
        }
}

 public static void CommitFrequency(UserRepo repository)
    {
        var listOfUniqueDates = (
        from author in repository.CommitList
        orderby author.aDate
        group author by new { author.aDate.Date } into g
        select new { date = g.Key, count = g.Count() }
     );

        foreach (var output in listOfUniqueDates)
        {
            Console.WriteLine(output.count + " " + output.date.Date.ToString("dd-MM-yyyy"));
        }

    }

    public static void CommitAuthor(UserRepo repository)
    {
        var authorNameList = (
           (from author in repository.CommitList
            orderby author.Author
            select new { name = author.Author }).Distinct()
        );
        
        foreach (var distAuthor in authorNameList)
        {
            Console.WriteLine(distAuthor.name);

            var listOfAuthorsCommitHistorie = (
            from author in repository.CommitList
            where author.Author == distAuthor.name
            orderby author.aDate.Date
            group author by new
            {
                author.aDate.Date
            } into g
            select new { key = g.Key, count = g.Count() }
            );
            foreach (var comDate in listOfAuthorsCommitHistorie)
            {
                Console.WriteLine("      " + comDate.count + " " + comDate.key.Date.ToString("dd-MM-yyyy"));
                
            }
        }
    }
    public static DateTimeOffset latestCommit(String pathName)
    {   
        var repository = new UserRepo(pathName);
        var result = repository.CommitList.OrderByDescending(x => x.aDate).First();
        Console.WriteLine("result: " + result.aDate);
        return result.aDate;
    }
}
