namespace Project.Tests;

using System.IO.Compression;
using LibGit2Sharp;
using Project.Core;
using Project.Infrastructure;
// Tests that the program prints the correct results
public class ComAuthorResultRepoTests
{

    /*public Repository repo;
    private UserRepo _userRepo;
    private StringWriter? _writer;*/

    private readonly SqliteConnection _connection;
    private readonly ProjectContext _context;

    private readonly ComAuthorResultRepo _repository;

    public ComAuthorResultRepoTests() 
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectContext>().UseSqlite(_connection);
        _context = new ProjectContext(builder.Options);
        _context.Database.EnsureCreated();

        var ComAuthRes1 = new ComAuthorResult {
            Id = 1,
            Author = "TestAuth1",
            CommitCount = 3,
            CommitDate = new DateTime(2022, 11, 20),
            RepositoryId = 1,
        };
            

         var ComAuthRes2= new ComAuthorResult {
            Id = 2,
            Author = "TestAuth2",
            CommitCount = 4,
            CommitDate = new DateTime(2022,10, 6),
            RepositoryId = 1
            
        };
        _context.AuthorResults.AddRange(ComAuthRes1,ComAuthRes2);
        
        var GitHubArch1 = new GitHubArchive {
            RepositoryName = "TestArchive",
            Id = 1,
            LatestCommit = new DateTimeOffset(new DateTime(2022, 11, 20)),
        };

        _context.Repositories.Add(GitHubArch1);

        
        _context.SaveChanges();

        _repository = new ComAuthorResultRepo(_context);
    
    }

    
    [Fact]

    public void Create_Succes(){
       
        var (response, createdId) = _repository.Create(new CreateComAuthorResultDTO("Baldur",2,new DateTime(2022, 11, 18), 1));

        response.Should().Be(Response.Created);

        createdId.Should().Be(3);

    }

    [Fact] 
    public void Create_alreadyExists_Should_Update (){
        
        var (response, createdId) = _repository.Create(new CreateComAuthorResultDTO("TestAuth2",5,new DateTime(2022,10, 6), 1));

        response.Should().Be(Response.Updated);

        createdId.Should().Be(2);
        
    }

    [Fact] 
    public void Find_Succes (){
        
        var actual = _repository.Find(2);

        actual.Should().BeEquivalentTo(new ComAuthorResultDTO(2,"TestAuth2" ,4,new DateTime(2022,10, 6), 1));
    }

     [Fact] 
    public void Find_Failure (){
        _repository.Find(22).Should().Be(null);
    }

[Fact] 
    public void Update_succes_nochanges (){

        var actualResponse = _repository.Update(new UpdateComAuthorResultDTO(1,3));

        actualResponse.Should().Be(Response.Updated);

        _repository.Find(1).CommitCount.Should().Be(3);


    }
    [Fact] 
    public void Update_succes_changes (){

        var actualResponse = _repository.Update(new UpdateComAuthorResultDTO(1,6));

        actualResponse.Should().Be(Response.Updated);

        _repository.Find(1)!.CommitCount.Should().Be(6);

    }

    [Fact] 
    public void Update_Fail (){

        _repository.Update(new UpdateComAuthorResultDTO(22,3)).Should().Be(Response.NotFound);

    }


    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
        
    }
}