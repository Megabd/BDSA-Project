namespace Project.Tests;
using LibGit2Sharp;
using Project.Core;
using Project.Infrastructure;
// Tests that the program prints the correct results
public class CommitFrequencyResultRepoTests
{

    public Repository repo;
    private UserRepo _userRepo;

    private readonly SqliteConnection _connection;
    private readonly ProjectContext _context;

    private readonly ComFrequencyResultRepo _repository;

    private readonly int myRepoId;
    public CommitFrequencyResultRepoTests() 
    {


        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectContext>().UseSqlite(_connection);
        _context = new ProjectContext(builder.Options);
        _context.Database.EnsureCreated();

        Repository.Init("coolRepo");
        repo = new Repository("coolRepo");
        
        var commitOptions = new CommitOptions();
        commitOptions.AllowEmptyCommit = true;

        //DateTime(Year, month, day, hour, minute, day)
        DateTimeOffset dateTimeOffset = new DateTimeOffset(new DateTime(2008, 5, 1, 8, 30, 52));
        Signature baldur = new Signature("Baldur", "bath@itu.dk", dateTimeOffset);
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

        _repository = new ComFrequencyResultRepo(_context);
    }

    
    [Fact]

    public void Create_Succes(){
        var (response, createdId) = _repository.Create(new CreateComFrequencyResultDTO(1,new DateTime(2022, 11, 18), myRepoId));

        response.Should().Be(Response.Created);

        createdId.Should().Be(4);
    }

    [Fact] 
    public void Create_alreadyExists (){
        
        var (response, createdId) = _repository.Create(new CreateComFrequencyResultDTO(1,new DateTime(2009, 5, 1), myRepoId));

        response.Should().Be(Response.Updated);
        
        createdId.Should().Be(2);
    }

    [Fact] 
    public void Find_Succes (){
        
        var expected = new ComFrequencyResultDTO(2, 2,new DateTime(2009, 5, 1), myRepoId);

        var actual = _repository.Find(2);

        actual.Should().BeEquivalentTo(expected);

    }

     [Fact] 
    public void Find_Failure (){

        var actual = _repository.Find(22);

        actual.Should().Be(null);


    }

    [Fact] 
    public void Update_succes_nochanges (){

        var  actualValue = _repository.Find(2)!.CommitCount;
        var actualResponse = _repository.Update(new UpdateComFrequencyResultDTO(2,2));

        actualResponse.Should().Be(Response.Updated);

        _repository.Find(2)!.CommitCount.Should().Be(actualValue);

    }
    [Fact] 
    public void Update_succes_changes (){

        var actual = _repository.Update(new UpdateComFrequencyResultDTO(2,3));

        actual.Should().Be(Response.Updated);

        _repository.Find(2)!.CommitCount.Should().Be(3);

    }

 [Fact] 
    public void Update_Fail (){

        var actual = _repository.Update(new UpdateComFrequencyResultDTO(7,1));

        actual.Should().Be(Response.NotFound);

    }

    public void Dispose()
    {

        DeleteReadOnlyDirectory("coolRepo");
            
           
        _connection.Dispose();
        repo.Dispose(); 
        _context.Dispose();

        
    }

    public static void DeleteReadOnlyDirectory(string directory)
{
    foreach (var subdirectory in Directory.EnumerateDirectories(directory)) 
    {
        DeleteReadOnlyDirectory(subdirectory);
    }
    foreach (var fileName in Directory.EnumerateFiles(directory))
    {
        var fileInfo = new FileInfo(fileName);
        fileInfo.Attributes = FileAttributes.Normal;
        fileInfo.Delete();
    }
    Directory.Delete(directory);
}
}