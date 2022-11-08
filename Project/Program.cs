using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Project.Infrastructure;
namespace Project;

public class Program
{
    // See https://aka.ms/new-console-template for more information
    public static void Main(string[] args)
    {
        String connectionString = "Server=localhost,1433; Database=msdb; User Id=SA; Password=Docker@Password";
        var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var context = new ProjectContext(optionsBuilder.Options);
        //EnvironmentVariableTarget connected = ContextBoundObject.Database.CanConnect();
        //Console.WriteLine(connected.ToString());

        Console.WriteLine("Please enter path to repository");
        var pathName = Console.ReadLine();

        var repo = new UserRepo(pathName!);

        Console.WriteLine("Press 1 for Commit fequency. Press 2 for Commit Author");
        string consoleInput = Console.ReadLine()!;
        if (consoleInput == "1")
        {
           RepositoryMethods.CommitFrequency(repo);
        }
        else if (consoleInput == "2")
        {
            RepositoryMethods.CommitAuthor(repo);
        }


    }

}



