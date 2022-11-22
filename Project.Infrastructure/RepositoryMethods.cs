using LibGit2Sharp;
using Project.Core;
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

 public static Dictionary<String, int> CommitFrequency(UserRepo archive, ProjectContext context)
    {

        var results = new Dictionary<String, int>();
    
    var archiveRepo = new GitHubArchiveRepo(context);
    var GitHubArch = new CreateGitHubArchiveDTO(archive.Name);
    var createResponse = archiveRepo.Create(GitHubArch);

    var comFrequencyResultRepo = new ComFrequencyResultRepo(context);

        var listOfUniqueDates = (
        from author in archive.CommitList
        orderby author.aDate
        group author by new { author.aDate.Date } into g
        select new { date = g.Key, count = g.Count() }
     );

        foreach (var output in listOfUniqueDates)
        {   
            var createComFreResult = new CreateComFrequencyResultDTO(output.count,output.date.Date,createResponse.GitHubArchiveId);
            comFrequencyResultRepo.Create(createComFreResult);
            Console.WriteLine(output.count + " " + output.date.Date.ToString("dd-MM-yyyy"));
            results.Add(output.date.Date.ToString("dd-MM-yyyy"), output.count);

        }

     var date = latestCommit(archive.Name);
     var updateAchiveRepo = new UpdateGitHubArchiveDTO(createResponse.GitHubArchiveId,date);
     var updateResponse = archiveRepo.Update(updateAchiveRepo);

     return results;
    }

    public static Dictionary<String, Dictionary<String, int>> CommitAuthor(UserRepo archive, ProjectContext context)
    {

        var results = new Dictionary<String, Dictionary<String, int>>();

        var archiveRepo = new GitHubArchiveRepo(context);
        var GitHubArch = new CreateGitHubArchiveDTO(archive.Name);
        var createResponse = archiveRepo.Create(GitHubArch);

        var comAuthResultRepo = new ComAuthorResultRepo(context);

        var authorNameList = (
           (from author in archive.CommitList
            orderby author.Author
            select new { name = author.Author }).Distinct()
        );
        
        foreach (var distAuthor in authorNameList)
        {
            Console.WriteLine(distAuthor.name);
            var authorCommitsDict = new Dictionary<String, int>();

            var listOfAuthorsCommitHistorie = (
            from author in archive.CommitList
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
                var createComAuthResult = new CreateComAuthorResultDTO(distAuthor.name,comDate.count, comDate.key.Date,createResponse.GitHubArchiveId);
                comAuthResultRepo.Create(createComAuthResult);
                Console.WriteLine("      " + comDate.count + " " + comDate.key.Date.ToString("dd-MM-yyyy"));
                authorCommitsDict.Add(comDate.key.Date.ToString("dd-MM-yyyy"), comDate.count);
            }
            results.Add(distAuthor.ToString()!, authorCommitsDict);
        }

        var date = latestCommit(archive.Name);
        var updateAchiveRepo = new UpdateGitHubArchiveDTO(createResponse.GitHubArchiveId,date);
        var updateResponse = archiveRepo.Update(updateAchiveRepo);

        return results;
    }
    public static DateTimeOffset latestCommit(String pathName)
    {   
        var repository = new UserRepo(pathName);
        var result = repository.CommitList.OrderByDescending(x => x.aDate).First();
        Console.WriteLine("result: " + result.aDate);
        return result.aDate;
    }


    public static void UpdateRepository (String pathName) {
        var repository = new UserRepo(pathName);
        

    }
}
