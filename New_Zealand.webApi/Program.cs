using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using New_Zealand.webApi.Data;
using New_Zealand.webApi.Mappings;
using New_Zealand.webApi.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//injection de notre dbContext
builder.Services.AddDbContext<New_ZealandDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LinkCs")));

builder.Services.AddDbContext<New_ZealandAuthDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionStrings")));


//injection de IRegionRepository et SQLRegionRepository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalksRepository, SQLWalksRepository>();

//injection de automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityUser>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<New_ZealandAuthDbContext>()
    .AddDefaultTokenProviders();


//configuration des paramètre du mot de passe
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit= false;
    options.Password.RequireLowercase= false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength= 6;
    options.Password.RequiredUniqueChars= 1;
});


//injection de jwtBearer(Authentication)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//j'ai ajouté UseAuthentication()
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
