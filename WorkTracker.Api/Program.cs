using Microsoft.EntityFrameworkCore;
using WorkTracker.Api.Data;
using WorkTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("WorkTracker") ?? throw new InvalidOperationException("Connection string 'WorkTracker' was not found.");

builder.Services.AddDbContext<WorkTrackerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<IWorkItemService, WorkItemService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVite",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowVite");

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
