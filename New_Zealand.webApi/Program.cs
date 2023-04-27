using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using New_Zealand.webApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//injection de notre dbContext
builder.Services.AddDbContext<New_ZealandDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LinkCs")));
/*builder.Services.AddDbContext<New_ZealandDbContext>(Option => {
    Option.UseSqlServer(builder.Configuration.GetConnectionString("LinkCs"));
});*/


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
