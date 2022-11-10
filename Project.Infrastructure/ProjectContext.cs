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

    }

}