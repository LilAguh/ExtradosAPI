using DataAccess.Implementations;
using DataAccess.Interfaces;
using ExtradosApi.Services;
using ExtradosApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserDAO, UserDAO>();
builder.Services.AddScoped<IUserService, UserService>();


//paso 1: configurar la validación por JWT

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourcode.com",   // “yourco.com” es el issuer/audience
            ValidAudience = "yourcode.com", // “yourco.com” es el issuer/audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_secret_secret"))
            //codigo secreto que se debe guardar
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

 //paso 2: declarar que vamos a utilizar el middleware de autorización y autenticación de .net

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
