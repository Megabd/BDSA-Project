using LibGit2Sharp;

namespace Project.Infrastructure;

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
    
            this.Name = pathName.Substring(pathName.LastIndexOf('/') + 1);
            Console.WriteLine(Name);
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

       this.Name = repo.Info.Path.Substring(repo.Info.Path.LastIndexOf('/') + 1);

        foreach (var Author in commits.Select(com => new Commit(com.Author.Name, com.Author.When)))
        {
            if (!CommitList.Contains(Author))
            {
                CommitList.Add(Author);
            }
        }
    }


    
}
