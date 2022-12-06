namespace Project.Tests;
using LibGit2Sharp;
using Project.Core;
using Project.Infrastructure;
// Tests that the program prints the correct results
public class CommitFrequencyResultRepoTests
{

    private readonly SqliteConnection _connection;
    private readonly ProjectContext _context;

    private readonly ComFrequencyResultRepo _repository;

    public CommitFrequencyResultRepoTests() 
    {


        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectContext>().UseSqlite(_connection);
        _context = new ProjectContext(builder.Options);
        _context.Database.EnsureCreated();

         var ComFreqRes1 = new ComFrequencyResult {
            Id = 1,
            CommitCount = 10,
            CommitDate = new DateTime(2022, 11, 20),
            RepositoryId = 1,
        };
            

         var ComFreqRes2= new ComFrequencyResult {
            Id = 2,
            CommitCount = 8,
            CommitDate = new DateTime(2022,10, 6),
            RepositoryId = 1
            
        };
        _context.FrequencyResults.AddRange(ComFreqRes1,ComFreqRes2);
        
        var GitHubArch1 = new GitHubArchive {
            RepositoryName = "TestArchive",
            Id = 1,
            LatestCommit = new DateTimeOffset(new DateTime(2022, 11, 20)),
        };

        _context.Repositories.Add(GitHubArch1);
        _context.SaveChanges();

        _repository = new ComFrequencyResultRepo(_context);
    }

    
    [Fact]

    public void Create_Succes(){
        var (response, createdId) = _repository.Create(new CreateComFrequencyResultDTO(1,new DateTime(2022, 11, 18), 1));

        response.Should().Be(Response.Created);

        createdId.Should().Be(3);


    }

    [Fact] 
    public void Create_alreadyExists (){
        
        var (response, createdId) = _repository.Create(new CreateComFrequencyResultDTO(20,new DateTime(2022, 11, 20), 1));

        response.Should().Be(Response.Updated);
        
        createdId.Should().Be(1);


    }

    [Fact] 
    public void Find_Succes (){
        
        var expected = new ComFrequencyResultDTO(1, 10,new DateTime(2022, 11, 20), 1);

        var actual = _repository.Find(1);

        actual.Should().BeEquivalentTo(new ComFrequencyResultDTO(1, 10,new DateTime(2022, 11, 20), 1));



    }

     [Fact] 
    public void Find_Failure (){

        var actual = _repository.Find(22);

        actual.Should().Be(null);



    }

    [Fact] 
    public void Update_succes_nochanges (){

        
        var actualResponse = _repository.Update(new UpdateComFrequencyResultDTO(2,2));

        actualResponse.Should().Be(Response.Updated);

        _repository.Find(2)!.CommitCount.Should().Be(8);


    }
    [Fact] 
    public void Update_succes_changes (){

        var actual = _repository.Update(new UpdateComFrequencyResultDTO(2,50));

        actual.Should().Be(Response.Updated);

        _repository.Find(2)!.CommitCount.Should().Be(50);


    }

 [Fact] 
    public void Update_Fail (){

        var actual = _repository.Update(new UpdateComFrequencyResultDTO(7,1));

        actual.Should().Be(Response.NotFound);


    }


    public void Dispose()
    { 
        _connection.Dispose();
        _context.Dispose();
    }
}