
using Microsoft.AspNetCore.Builder;
using DeptAssignment.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching(options => options.MaximumBodySize = 1024);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
var movieApiKey = builder.Configuration["TheMoviesDB:APIKey"];
var app = builder.Build();
/*  */
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

APIDataHandler.InitializeAPI(movieApiKey);

app.Run();
