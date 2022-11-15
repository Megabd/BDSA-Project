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

//app.MapGet("/{RepositoryPath}", async (string path, UserRepo repository) => await repository.FindAsync());

 app.Run();
