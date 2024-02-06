using dotenv.net;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Database;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(options: new DotEnvOptions(ignoreExceptions: false));

// Add services to the container.

builder.Services.AddControllers();

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new InvalidOperationException();
// var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException();
// var jwtIssuer = Environment.GetEnvironmentVariable("JWT_SECRET_ISSUER") ?? throw new InvalidOperationException();
// var jwtAudience = Environment.GetEnvironmentVariable("JWT_SECRET_AUDIENCE") ?? throw new InvalidOperationException();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(databaseUrl));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
