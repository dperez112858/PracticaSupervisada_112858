using System.Net.Mime;
using System;
using Microsoft.EntityFrameworkCore;
using API.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql()
.AddDbContext<Context>(options => options.UseNpgsql (builder.Configuration.GetConnectionString("ConexionDatabase")));


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c=>{ 
    c.AllowAnyHeader(); 
    c.AllowAnyMethod();
     c.AllowAnyOrigin(); 
     });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
