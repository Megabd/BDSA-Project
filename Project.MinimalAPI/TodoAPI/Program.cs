using Project.Infrastructure;
using Project.Core;
using LibGit2Sharp;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectContext>();
builder.Services.AddScoped<IComAuthorResultRepo, ComAuthorResultRepo>();
builder.Services.AddScoped<IComFrequencyResultRepo, ComFrequencyResultRepo>();
builder.Services.AddScoped<IGitHubArchiveRepo, GitHubArchiveRepo>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

/* var commitAuthor = app.MapGroup("/commitAuthor").WithOpenApi();
var commitFrequency = app.MapGroup("/commitFrequency").WithOpenApi(); */

app.MapGet("/author/{RepositoryOwner}/{RepositoryName}", (string RepositoryOwner, string RepositoryName, ProjectContext context) => {
    var repo = ApiMethods.CloneRepo(RepositoryOwner, RepositoryName);
    var userRepo = new UserRepo(repo);
    var results = RepositoryMethods.CommitAuthor(userRepo, context);
    return results;

});

app.MapGet("/frequency/{RepositoryOwner}/{RepositoryName}", (string RepositoryOwner, string RepositoryName, ProjectContext context) => {
    var repo = ApiMethods.CloneRepo(RepositoryOwner, RepositoryName);
    var userRepo = new UserRepo(repo);
    var results = RepositoryMethods.CommitFrequency(userRepo, context);
    return results;
});

app.MapGet("/forks/{RepositoryOwner}/{RepositoryName}", (string RepositoryOwner, string RepositoryName, ProjectContext context) => {
    var results = ApiMethods.GetForks(RepositoryOwner, RepositoryName);
    return results;
});

app.Run();
