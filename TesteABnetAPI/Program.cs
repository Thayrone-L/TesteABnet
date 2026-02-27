using BackEndTesteABnet.Data;
using BackEndTesteABnet.Repository;
using Microsoft.EntityFrameworkCore;
using TesteABnetAPI.Interfaces;
using TesteABnetAPI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=assignments.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend",
        policy =>
        {
            policy.WithOrigins("https://localhost:7069")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AssignmentServiceInterface, AssignmentService>();
builder.Services.AddScoped<AssignmentRepository>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
