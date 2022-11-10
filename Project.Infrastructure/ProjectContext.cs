using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Project;

namespace Project.Infrastructure;

public class ProjectContext : DbContext
{
    public DbSet<Commit> Commits => Set<Commit>();
    public DbSet<UserRepo> Repositories => Set<UserRepo>();
    


    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Commit>().HasIndex(c=>c.Id).IsUnique();
        modelBuilder.Entity<UserRepo>().HasIndex(r=>r.Id).IsUnique();

    }

}