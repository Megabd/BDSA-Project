
namespace Project.Tests;

public class UnitTest1
{
    [Fact]
    // Uses https://github.com/katrinesando/BDSA_assignment-02
    public void Give_Repository_return_nr_of_commits_total_per_Date()
    {
        //Arrange
        using var writer = new StringWriter();
        Console.SetOut(writer);
        Console.WriteLine("5 21-09-2022 \n 3 20-09.2022 \n 1 19-09-2022 \n 1 18-09-2022 \n 4 16-09-2022 \n 2 15-09-2022 \n 1 14-09-2022 \n 5 17-09-2021 \n 2 16-09-2021");
        var output = writer.GetStringBuilder().ToString().TrimEnd();
        //Act
        
        //Assert
        output.Should().Contain("5 21-09-2022");
        output.Should().Contain("3 20-09.2022");
        output.Should().Contain("1 19-09-2022");
        output.Should().Contain("1 18-09-2022");
        output.Should().Contain("4 16-09-2022");
        output.Should().Contain("2 15-09-2022");
        output.Should().Contain("1 14-09-2022");
        output.Should().Contain("5 17-09-2021");
        output.Should().Contain("2 16-09-2021"); 
    }

    [Fact]
    public void Give_Repository_return_nr_of_commits_per_Author_per_Date()
    {
        //Arrange
        using var writer = new StringWriter();
        Console.SetOut(writer);
        Console.WriteLine("FirstName LastName \n 5 21-09-2023");
        Console.WriteLine("");
        Console.WriteLine("FirstName LastName \n 5 21-09-2022");

        var output = writer.GetStringBuilder().ToString().TrimEnd();
        //Act
        var outputs = output.Split(new string[] { "\r\n\r\n" },
                               StringSplitOptions.RemoveEmptyEntries); 


        //Assert
        outputs[0].Should().Contain("FirstName LastName");
        outputs[0].Should().Contain("5 21-09-2023");
        outputs[1].Should().Contain("FirstName LastName");
        outputs[1].Should().Contain("5 21-09-2022");
     
    }    
}