using Project.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

/* builder.Services.AddDbContext<ProjectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Project")));
builder.Services.AddScoped<IComAuthorResultRepo, ComAuthorResultRepo>();
builder.Services.AddScoped<IComFrequencyResultRepo, ComFrequencyResultRepo>();
builder.Services.AddScoped<IGitHubArchiveRepo, GitHubArchiveRepo>(); */

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

app.MapGet("/", () => "Hello World!");

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
