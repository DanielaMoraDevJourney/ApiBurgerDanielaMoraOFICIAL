using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiBurgerDanielaMora.Data;
using ApiBurgerDanielaMora.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiBurgerDanielaMoraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiBurgerDanielaMoraContext") ?? throw new InvalidOperationException("Connection string 'ApiBurgerDanielaMoraContext' not found.")));

// Add services to the container.

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

app.MapBurgerEndpoints();

app.Run();
