namespace Project.Tests;
using LibGit2Sharp;
using Project.Core;
using Project.Infrastructure;
// Tests that the program prints the correct results
public class GitHubArchiveRepoTests
{

    public Repository repo;
    private UserRepo _userRepo;
    private StringWriter? _writer;

    private readonly SqliteConnection _connection;
    private readonly ProjectContext _context;

    private readonly GitHubArchiveRepo _repository;

    private readonly int myRepoId;
    public GitHubArchiveRepoTests() 
    {


        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectContext>().UseSqlite(_connection);
        _context = new ProjectContext(builder.Options);
        _context.Database.EnsureCreated();

        Repository.Init("./coolRepo");
        Repository.Init("./funnyRepo");
        repo = new Repository("./coolRepo");
        var repo2 = new Repository("./funnyRepo");
        
        var commitOptions = new CommitOptions();
        commitOptions.AllowEmptyCommit = true;

        DateTimeOffset dateTimeOffsetRepo2 = new DateTimeOffset(new DateTime(2008, 5, 1, 8, 30, 52));
        Signature baldur = new Signature("Baldur", "bath@itu.dk", dateTimeOffsetRepo2);
        repo2.Commit("Initial commit", baldur, baldur, commitOptions);

        //DateTime(Year, month, day, hour, minute, day)
        DateTimeOffset dateTimeOffset = new DateTimeOffset(new DateTime(2008, 5, 1, 8, 30, 52));
        Signature baldurRepo2 = new Signature("Baldur", "bath@itu.dk", dateTimeOffset);
        repo.Commit("Initial commit", baldur, baldur, commitOptions);

        DateTimeOffset dateTimeOffset2 = new DateTimeOffset(new DateTime(2008, 5, 1, 7, 30, 52));
        Signature baldur2 = new Signature("Baldur", "bath@itu.dk", dateTimeOffset2);
        repo.Commit("Duh", baldur2, baldur2, commitOptions);
        
        DateTimeOffset dateTimeOffset3 = new DateTimeOffset(new DateTime(2088, 5, 1, 12, 35, 40));
        Signature baldur3 = new Signature("Baldur", "bath@itu.dk", dateTimeOffset3);
        repo.Commit("Fixes to Duh", baldur3, baldur3, commitOptions);
        
        DateTimeOffset dateTimeOffset4 = new DateTimeOffset(new DateTime(2009, 5, 1, 8, 30, 52));
        Signature benjamin = new Signature("Benjamin", "bhag@itu.dk", dateTimeOffset4);
        repo.Commit("Best commit", benjamin, benjamin, commitOptions);

        DateTimeOffset dateTimeOffset5 = new DateTimeOffset(new DateTime(2009, 5, 1, 12, 20, 41));
        Signature nicholas = new Signature("Nicholas", "nicha@itu.dk", dateTimeOffset5);
        repo.Commit("Foo commit", nicholas, nicholas, commitOptions);

        _userRepo = new UserRepo(repo);

        
        RepositoryMethods.CommitFrequency(_userRepo, _context);
        var archiveRepo = new GitHubArchiveRepo(_context);
        var GitHubArch = new CreateGitHubArchiveDTO(_userRepo.Name);
        myRepoId = archiveRepo.Create(GitHubArch).GitHubArchiveId;
        _context.SaveChanges();

        _repository = new GitHubArchiveRepo(_context);
    }

    
    [Fact]

    public void Create_Succes(){
        
        var actual = _repository.Create(new CreateGitHubArchiveDTO("./funnyRepo"));

        actual.Response.Should().Be(Response.Created);
        actual.GitHubArchiveId.Should().Be(2);

        Dispose();

    }

    [Fact] 
    public void Create_alreadyExists (){
        
        var actual = _repository.Create(new CreateGitHubArchiveDTO(_repository.Find(1)!.RepositoryName));
        
        actual.Response.Should().Be(Response.Conflict);
        actual.GitHubArchiveId.Should().Be(1);

        Dispose();

    }

    [Fact] 
    public void Find_Succes (){
        // Don't know why gitlib2sharp gives this name
        _repository.Find(1).Should().BeEquivalentTo(new GitHubArchiveDTO(1,"c:\\Users\\Baldur Thomsen\\Analyse og Design\\BDSA-Project\\Project.Tests\\bin\\Debug\\net6.0\\coolRepo\\.git\\",new DateTimeOffset(new DateTime(2088, 5, 1, 12, 35, 40))));


        Dispose();

    }

     [Fact] 
    public void Find_Failure (){

        _repository.Find(22).Should().Be(null);


        Dispose();

    }

    [Fact] 
    public void Update_succes_nochanges (){
        var beforeDate = _repository.Find(1)!.LatestCommit;
        var newDate = new DateTimeOffset(new DateTime(2088, 5, 1, 12, 35, 40));
        var actual = _repository.Update(new UpdateGitHubArchiveDTO(1, newDate));

        actual.Should().Be(Response.Updated);
       _repository.Find(1)!.LatestCommit.Should().Be(beforeDate);
        
        Dispose();

    }
    [Fact] 
    public void Update_succes_changes (){

        var newDate = new DateTimeOffset(new DateTime(2089, 5, 1, 12, 35, 40));
        var actual = _repository.Update(new UpdateGitHubArchiveDTO(1, newDate));

        actual.Should().Be(Response.Updated);
       _repository.Find(1)!.LatestCommit.Should().Be(newDate);

        Dispose();

    }

 [Fact] 
    public void Update_Fail (){

        var newDate = new DateTimeOffset(new DateTime(2088, 5, 1, 12, 35, 40));
        var actual = _repository.Update(new UpdateGitHubArchiveDTO(4, newDate));

        actual.Should().Be(Response.NotFound);

        Dispose();

    }


    public void Dispose()
    {
        System.IO.DirectoryInfo di = new DirectoryInfo("./coolRepo");
        System.IO.DirectoryInfo di2 = new DirectoryInfo("./funnyRepo");

            foreach (FileInfo file in di.GetFiles())
            {
                 file.Delete(); 
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try{
                dir.Delete(true); 
                }
                catch (Exception e){
                    Console.WriteLine(e.Message);
                }
            }

            foreach (FileInfo file in di2.GetFiles())
            {
                 file.Delete(); 
            }
            foreach (DirectoryInfo dir in di2.GetDirectories())
            {
                try{
                dir.Delete(true); 
                }
                catch (Exception e){
                    Console.WriteLine(e.Message);
                }
            }
           

        repo.Dispose(); 
        _connection.Dispose();
        _context.Dispose();

        
    }
}