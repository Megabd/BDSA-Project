namespace Project.Tests;
using LibGit2Sharp;
using Project;
using Project.Infrastructure;

   
public class UserRepoRepoTests
{
    private readonly SqliteConnection _connection;
    private readonly ProjectContext _context;
    private readonly UserRepoRepo _repository;

    //private readonly SqliteConnection _connection;

    public UserRepoRepoTests() 
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

        _repository = new UserRepoRepo(_context);
    }
    
    }