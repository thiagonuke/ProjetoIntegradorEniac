using Desafio_Processo.API.Domain;
using Desafio_Processo.API.Interface;
using Desafio_Processo.API.Models;
using Desafio_Processo.UI.RequestAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILojaRepository, LojaRepository>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("sqllitedb")));

builder.Services.AddTransient<IDbConnection>(sp =>
{
    IConfiguration configuration = sp.GetRequiredService<IConfiguration>();
    string connectionString = configuration.GetConnectionString("sqllitedb");
    return new SqlConnection(connectionString);
});

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
