using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Text;
using ToDoApp;
using ToDoApp.Data;
using ToDoApp.Tasks;
using ToDoApp.Tasks.Handlers;
using ToDoApp.Users.Handlers;
using ToDoApp.Users.IdAcess;
using ToDoApp.Users;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference(
                    "Bearer",
                    document
                ),
               new List<string>()
            }
        });
});

services.AddControllers();

string databasePath = Path.Combine(builder.Environment.ContentRootPath, "todo-ef.db");
string connectionString = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder
{
    DataSource = databasePath
}.ToString();

services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
services.AddSingleton(new Database(connectionString));
services.AddSingleton<Logger>();

services.AddTransient<ITaskStorage, SQliteTasksStorage>();
services.AddTransient<IUserStorage, UserStorage>();

services.AddTransient<RegisterUserCommandHandler>();
services.AddTransient<CreateTaskCommandHandler>();
services.AddTransient<GetTasksCommandHandler>();
services.AddTransient<DeleteTaskCommandHandler>();
services.AddTransient<DoneTaskCommandHandler>();
services.AddTransient<LoginUserCommandHandler>();
services.AddTransient<SearchTasksCommandHandler>();
services.AddTransient<SearchOverdueTasksCommandHandler>();

// Scoped!
services.AddHttpContextAccessor();
services.AddScoped<ICurrentUserContext, CurrentUserContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AuthConstants.Issuer,
            ValidAudience = AuthConstants.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.Secret))
        };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
