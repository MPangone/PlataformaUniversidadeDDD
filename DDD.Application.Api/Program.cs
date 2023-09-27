using DDD.Infra.MemoryDb;
using DDD.Infra.SqlServer;
using DDD.Infra.SqlServer.Interfaces;
using DDD.Infra.SqlServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//IOC - Dependency Injection
//builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepositorySqlServer>();
builder.Services.AddScoped<SqlContext, SqlContext>();
builder.Services.AddScoped<IDisciplinaRepository, DisciplinaRepositorySqlServer>();

builder.Services.AddControllers();
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
