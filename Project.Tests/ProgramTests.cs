namespace Project.Tests;
using LibGit2Sharp;
using Project.Core;
using Project.Infrastructure;

public class UserRepoTests
{

    public Repository repo;
    private UserRepo _userRepo;
    private StringWriter? _writer;

    public UserRepoTests() 
    {
        Repository.Init("./coolRepo");
        repo = new Repository("./coolRepo");
        
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
        _userRepo.CommitFrequency();
        var output = writer.GetStringBuilder().ToString().TrimEnd();

        output.Should().Be("2 01-05-2008\r\n2 01-05-2009\r\n1 01-05-2088");
        Dispose();

    }

    [Fact]
    public void Print_commits_by_author() 
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        _userRepo.CommitAuthor();
        var output = writer.GetStringBuilder().ToString().TrimEnd();

        output.Should().Be("Baldur\r\n      2 01-05-2008\r\n      1 01-05-2088\r\nBenjamin\r\n      1 01-05-2009\r\nNicholas\r\n      1 01-05-2009");
        Dispose();

    }

    [Fact]

    public void Has_Name_coolRepo()
    {
        var userRepo = new UserRepo(repo);

        userRepo.Name.Should().Be("coolRepo");

        Dispose();

    }

    public void Dispose()
    {
        System.IO.DirectoryInfo di = new DirectoryInfo("./coolRepo");

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
           

        repo.Dispose(); 
        
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

