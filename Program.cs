using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using ToDoApp;
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

services.AddSingleton(new Database("Data Source=todo.db"));
services.AddSingleton<Logger>();

services.AddTransient<ITaskStorage, SQliteTasksStorage>();
services.AddTransient<IUserStorage, SqliteUserStorage>();


services.AddTransient<RegisterUserCommandHandler>();
services.AddTransient<CreateTaskCommandHandler>();
services.AddTransient<GetTasksCommandHandler>();
services.AddTransient<DeleteTaskCommandHandler>();
services.AddTransient<DoneTaskCommandHandler>();
services.AddTransient<LoginUserCommandHandler>();
services.AddTransient<SearchTasksCommandHandler>();

services.AddSingleton<UserSession>();


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
app.Services.GetRequiredService<Database>().EnsureCreated();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
