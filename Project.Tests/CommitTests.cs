namespace Project.Tests;
using LibGit2Sharp;
using Project;
using Project.Infrastructure;
// Tests that the program prints the correct results
public class CommitTests
{

    public Repository repo;
    private UserRepo _userRepo;
    private StringWriter? _writer;

    private readonly SqliteConnection _connection;
    private readonly ProjectContext _context;

    public CommitTests() 
    {


        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectContext>().UseSqlite(_connection);
        _context = new ProjectContext(builder.Options);
        _context.Database.EnsureCreated();

        //var bob = new UserRepo("Bob", "bob@mail.com") {Id = 1};

        //var tim = new User("Tim", "tim@mail.com") {Id = 2};

        //_context.Users.AddRange(bob, tim);
        _context.SaveChanges();




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
    }

    [Fact]
    public void Print_commit_frequency_returns_correct_amount_of_commits() 
    {
        using var writer = new StringWriter();
        Console.SetOut(writer); 

        RepositoryMethods.CommitFrequency(_userRepo, _context);

        var output = writer.GetStringBuilder().ToString().TrimEnd();

        output.Should().Contain("2 01-05-2008\r\n2 01-05-2009\r\n1 01-05-2088");

    }

    [Fact]
    public void Print_commits_by_author() 
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
    
        RepositoryMethods.CommitAuthor(_userRepo, _context);
        var output = writer.GetStringBuilder().ToString().TrimEnd();

        output.Should().Contain("Baldur\r\n      2 01-05-2008\r\n      1 01-05-2088\r\nBenjamin\r\n      1 01-05-2009\r\nNicholas\r\n      1 01-05-2009");

    }



    [Fact]

    public void Has_Name_coolRepo()
    {
        var userRepo = new UserRepo(repo);

        userRepo.Name.Should().Contain("coolRepo");

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

    // [Fact]
    // // Uses https://github.com/katrinesando/BDSA_assignment-02
    // public void Give_Repository_return_nr_of_commits_total_per_Date()
    // {
    //     //Arrange
    //     using var writer = new StringWriter();
    //     Console.SetOut(writer);
    //     Console.WriteLine("5 21-09-2022 \n 3 20-09.2022 \n 1 19-09-2022 \n 1 18-09-2022 \n 4 16-09-2022 \n 2 15-09-2022 \n 1 14-09-2022 \n 5 17-09-2021 \n 2 16-09-2021");
    //     var output = writer.GetStringBuilder().ToString().TrimEnd();
    //     //Act
        
    //     //Assert
    //     output.Should().Contain("5 21-09-2022");
    //     output.Should().Contain("3 20-09.2022");
    //     output.Should().Contain("1 19-09-2022");
    //     output.Should().Contain("1 18-09-2022");
    //     output.Should().Contain("4 16-09-2022");
    //     output.Should().Contain("2 15-09-2022");
    //     output.Should().Contain("1 14-09-2022");
    //     output.Should().Contain("5 17-09-2021");
    //     output.Should().Contain("2 16-09-2021"); 
    // }

    // [Fact]
    // public void Give_Repository_return_nr_of_commits_per_Author_per_Date()
    // {
    //     //Arrange
    //     using var writer = new StringWriter();
    //     Console.SetOut(writer);
    //     Console.WriteLine("FirstName LastName \n 5 21-09-2023");
    //     Console.WriteLine("");
    //     Console.WriteLine("FirstName LastName \n 5 21-09-2022");

    //     var output = writer.GetStringBuilder().ToString().TrimEnd();
    //     //Act
    //     var outputs = output.Split(new string[] { "\r\n\r\n" },
    //                            StringSplitOptions.RemoveEmptyEntries); 


    //     //Assert
    //     outputs[0].Should().Contain("FirstName LastName");
    //     outputs[0].Should().Contain("5 21-09-2023");
    //     outputs[1].Should().Contain("FirstName LastName");
    //     outputs[1].Should().Contain("5 21-09-2022");
     
    // }    
}

