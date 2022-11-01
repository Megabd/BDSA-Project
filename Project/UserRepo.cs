using LibGit2Sharp;

namespace Project;

public class UserRepo
{

    public List<Commit> CommitList{get;  set;}
    public string Name {get; set;}

    public int Id {get; set;}

    public UserRepo(string pathName)
    {
        CommitList = new List<Commit>();
        using (var repo = new Repository(pathName))
        {
            ///get commits from all branches, not just master
            var commits = repo.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = repo.Refs });

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

    public UserRepo(Repository repo)
    {
        CommitList = new List<Commit>();

        var commits = repo.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = repo.Refs });

        //here I can access commit's author, but not time

        foreach (var Author in commits.Select(com => new Commit(com.Author.Name, com.Author.When)))
        {
            if (!CommitList.Contains(Author))
            {
                CommitList.Add(Author);
            }
        }
    }


    public void CommitFrequency()
    {
        var listOfUniqueDates = (
        from author in CommitList
        orderby author.aDate
        group author by new { author.aDate.Date } into g
        select new { date = g.Key, count = g.Count() }
     );

        foreach (var output in listOfUniqueDates)
        {
            Console.WriteLine(output.count + " " + output.date.Date.ToString("dd-MM-yyyy"));
        }

    }

    public void CommitAuthor()
    {
        var authorNameList = (
           (from author in CommitList
            orderby author.Author
            select new { name = author.Author }).Distinct()
        );
        
        foreach (var distAuthor in authorNameList)
        {
            Console.WriteLine(distAuthor.name);

            var listOfAuthorsCommitHistorie = (
            from author in CommitList
            where author.Author == distAuthor.name
            orderby author.aDate
            group author by new
            {
                author.aDate,
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
}
