using Project.Infrastructure;
using Project.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=localhost;Port=5432;Database=BDSADatabase;User Id=postgres;Password=adam123;";

builder.Services.AddDbContext<ProjectContext>(options => options.UseNpgsql(connectionString));
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

app.MapGet("/", (ProjectContext context) => {
    var path = @"C:\Users\adamj\Documents\Central Vault\ITU\Noter\3. Semester\Analysis, Design and Software Architecture\Project\BDSA-Project";
    var userRepo = new UserRepo(path);
    RepositoryMethods.CommitFrequency(userRepo, context);
});

//app.MapGet("/{RepositoryOwner}/{RepositoryName}", (string RepositoryOwner, string RepositoryName) => RepositoryOwner + "/" + RepositoryName);


app.MapGet("/{action}", (int action) => {
    var dic = new Dictionary<DateTime, int>();
    if (action == 1) {
        dic.Add(DateTime.Now, 0);  
        dic.Add(DateTime.Now, 3);     
        dic.Add(DateTime.Now, 6); 
        dic.Add(DateTime.Now, 5); 
    } else {
        dic.Add(DateTime.Now, 0);  
        dic.Add(DateTime.Now, 0);     
        dic.Add(DateTime.Now, 1); 
        dic.Add(DateTime.Now, 5); 
    }
    return dic;
});


app.Run();
