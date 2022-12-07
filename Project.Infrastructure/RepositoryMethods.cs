using LibGit2Sharp;
using Project.Core;
using Octokit;
namespace Project.Infrastructure;


public class RepositoryMethods
{
    public static void addCommitsToRepository(string pathName)
    {
        List<Commit> CommitList = new List<Commit>();
        using (var repository = new LibGit2Sharp.Repository(pathName))
        {
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

    public static IQueryable<ComFrequencyResult> CommitFrequency(UserRepo archive, ProjectContext context)
    {

        var archiveRepo = new GitHubArchiveRepo(context);
        var GitHubArch = new CreateGitHubArchiveDTO(archive.Name, latestCommit(archive));
        var createResponse = archiveRepo.Create(GitHubArch);

        if (createResponse.Response == Response.Updated || createResponse.Response == Response.Created)
        {
            Update(archive, context, createResponse.GitHubArchiveId);
        }

        return FecthFreqData(createResponse.GitHubArchiveId, context);
    }

    public static IQueryable<ComAuthorResult> CommitAuthor(UserRepo archive, ProjectContext context)
    {

        var archiveRepo = new GitHubArchiveRepo(context);
        var GitHubArch = new CreateGitHubArchiveDTO(archive.Name, latestCommit(archive));
        var createResponse = archiveRepo.Create(GitHubArch);

        if (createResponse.Response == Response.Updated || createResponse.Response == Response.Created)
        {
            Update(archive, context, createResponse.GitHubArchiveId);
        }

        return FetchAuthorData(createResponse.GitHubArchiveId, context);
    }

    public static int RepoForks(UserRepo archive, ProjectContext context) {
        var archiveRepo = new GitHubArchiveRepo(context);
        var GitHubArch = new CreateGitHubArchiveDTO(archive.Name, latestCommit(archive));
        var createResponse = archiveRepo.Create(GitHubArch);

        GetForks(archive.Name, createResponse.GitHubArchiveId, context);
        return FetchForkData(createResponse.GitHubArchiveId, context).FirstOrDefault();
    }


    public static void Update(UserRepo archive, ProjectContext context, int archiveID)
    {
        var comAuthResultRepo = new ComAuthorResultRepo(context);

        var authorNameList = (
        (from author in archive.CommitList
         orderby author.Author
         select new { name = author.Author }).Distinct()
        );

        foreach (var distAuthor in authorNameList)
        {
            Console.WriteLine(distAuthor.name);
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
                var createComAuthResult = new CreateComAuthorResultDTO(distAuthor.name, comDate.count, comDate.key.Date, archiveID);
                comAuthResultRepo.Create(createComAuthResult);
            }


            //GetForks(archive.Name, archive.Id, context);

        }

        var comFrequencyResultRepo = new ComFrequencyResultRepo(context);
        var listOfUniqueDates = (
        from author in archive.CommitList
        orderby author.aDate
        group author by new { author.aDate.Date } into g
        select new { date = g.Key, count = g.Count() }
     );

        foreach (var output in listOfUniqueDates)
        {
            var createComFreResult = new CreateComFrequencyResultDTO(output.count, output.date.Date, archiveID);
            comFrequencyResultRepo.Create(createComFreResult);
        }

    }


    public static DateTimeOffset latestCommit(UserRepo archive)
    {
        var result = archive.CommitList.OrderByDescending(x => x.aDate).First();
        Console.WriteLine("result: " + result.aDate);
        return result.aDate;
    }

    public static IQueryable<ComAuthorResult> FetchAuthorData(int RepositoryId, ProjectContext context)
    {
        var result = from f in context.AuthorResults
                     where f.RepositoryId == RepositoryId
                     select f;

        return result;
    }

    public static IQueryable<ComFrequencyResult> FecthFreqData(int RepositoryId, ProjectContext context)
    {
        var result = from f in context.FrequencyResults
                     where f.RepositoryId == RepositoryId
                     select f;

        return result;
    }

    public static IQueryable<int> FetchForkData(int RepositoryId, ProjectContext context){
        var result = from f in context.Repositories
        where f.Id == RepositoryId
        select f.ForkAmount;


        return result;
    }


    public static async void GetForks(string repoName, int Id, ProjectContext context) {

        var client = new GitHubClient(new ProductHeaderValue("Github-insights"));
        var token = new Octokit.Credentials("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");
        client.Credentials = token;

        var repoNameSplit = repoName.Split(@"\");

        Console.WriteLine("REPONAME:");
        Console.WriteLine(repoName);
        Console.WriteLine(repoNameSplit[2] + repoNameSplit[3]);

        var forks = await client.Repository.Forks.GetAll(repoNameSplit[2], repoNameSplit[3]);
        //foreach (var fork in forks) {
            
        //}
        
        //var forkRepo = new ForkRepo(context);
        var archiveRepo = new GitHubArchiveRepo(context);
        var forkCount = forks.Count;
        archiveRepo.UpdateForkAmount(new UpdateGIthubArchiveForksDTO(Id, forkCount));
        
    }


}
