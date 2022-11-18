using Microsoft.EntityFrameworkCore;
namespace Project.Infrastructure;

public class ProjectContext : DbContext
{
    public DbSet<ComAuthorResult> AuthorResults => Set<ComAuthorResult>();
    public DbSet<ComFrequencyResult> FrequencyResults => Set<ComFrequencyResult>();
    public DbSet<GitHubArchive> Repositories => Set<GitHubArchive>();
    
    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<GitHubArchive>().HasIndex(g=>g.Id).IsUnique();
        modelBuilder.Entity<GitHubArchive>().HasIndex(g=>g.RepositoryName).IsUnique();
        modelBuilder.Entity<ComAuthorResult>().HasIndex(c=>c.Id).IsUnique();
        modelBuilder.Entity<ComFrequencyResult>().HasIndex(c=>c.Id).IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options){
        if(!options.IsConfigured){

            var connectionString = "Server=localhost;Port=5432;Database=BDSADatabase;User Id=postgres;Password=adam123;";
            options.UseNpgsql(connectionString);

        // var _connection = new Microsoft.Data.Sqlite.SqliteConnection("Filename=:memory:");
        // _connection.Open();
        // options.UseSqlite(_connection);

            // String connectionString = "Server=localhost,1433; Database=msdb; User Id=SA; Password=<Docker@Password";
            // options.UseSqlServer(connectionString);
        }
    }

}