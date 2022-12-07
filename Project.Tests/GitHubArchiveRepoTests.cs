namespace Project.Tests;
using LibGit2Sharp;
using Project.Core;
using Project.Infrastructure;
// Tests that the program prints the correct results
public class GitHubArchiveRepoTests {

    private readonly SqliteConnection _connection;
    private readonly ProjectContext _context;

    private readonly GitHubArchiveRepo _repository;

    public GitHubArchiveRepoTests() 
    {


        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectContext>().UseSqlite(_connection);
        _context = new ProjectContext(builder.Options);
        _context.Database.EnsureCreated();

        
        var GitHubArch1 = new GitHubArchive {
            RepositoryName = "TestArchive",
            Id = 1,
            LatestCommit = new DateTimeOffset(new DateTime(2022, 11, 20)),
        };
        var GitHubArch2 = new GitHubArchive {
            RepositoryName = "TestArchive2",
            Id = 2,
            LatestCommit = new DateTimeOffset(new DateTime(2022, 11, 21)),
        };


         _context.Repositories.AddRange(GitHubArch1, GitHubArch2);

        
        _context.SaveChanges();

        _repository = new GitHubArchiveRepo(_context);

       
        
    }

    
    [Fact]

    public void Create_Succes(){
        
        var actual = _repository.Create(new CreateGitHubArchiveDTO("FunnyRepo", new DateTimeOffset(new DateTime(2022, 12,12))));

        actual.Response.Should().Be(Response.Created);
        actual.GitHubArchiveId.Should().Be(3);

    }

    [Fact] 
    public void Create_alreadyExists (){
        
        var actual = _repository.Create(new CreateGitHubArchiveDTO("TestArchive", new DateTimeOffset(new DateTime(2022, 11, 20))));
        
        actual.Response.Should().Be(Response.Fetched);
        actual.GitHubArchiveId.Should().Be(1);

    }

    [Fact] 
    public void Find_Succes (){
        // Don't know why gitlib2sharp gives this name
        _repository.Find(1).Should().BeEquivalentTo(new GitHubArchiveDTO(1,"TestArchive", new DateTimeOffset(new DateTime(2022, 11, 20))));

    }

     [Fact] 
    public void Find_Failure (){

        _repository.Find(22).Should().Be(null);

    }

    [Fact] 
    public void Update_succes_nochanges (){
        var newDate = new DateTimeOffset(new DateTime(2008, 5, 1));
        var actual = _repository.Update(new UpdateGitHubArchiveDTO(1, newDate));

        actual.Should().Be(Response.Updated);
       _repository.Find(1)!.LatestCommit.Should().Be(new DateTimeOffset(new DateTime(2022, 11, 20)));

    }
    [Fact] 
    public void Update_succes_changes (){


        var actual = _repository.Update(new UpdateGitHubArchiveDTO(1, new DateTimeOffset(new DateTime(2089, 5, 1))));

        actual.Should().Be(Response.Updated);
       _repository.Find(1)!.LatestCommit.Should().Be(new DateTimeOffset(new DateTime(2089, 5, 1)));

    }

 [Fact] 
    public void Update_Fail (){

        var newDate = new DateTimeOffset(new DateTime(2088, 5, 1));
        var actual = _repository.Update(new UpdateGitHubArchiveDTO(4, newDate));

        actual.Should().Be(Response.NotFound);

    }


    public void Dispose()
    {
        _connection.Dispose();
        _context.Dispose();

        
    }
}