using BookStoreApp.API;
using BookStoreApp.API.Configuration;
using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Obtenir chaine de connexion
var connectionString = builder.Configuration.GetConnectionString("BookStoreDb");
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(MapperConfig));

// S�curit�

builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookStoreDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // quiconque veut acc�der � une ressource prot�g�e doit �tre authentifi� avec un Bearer
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // quiconque veut challenger un utilisateur doit �tre authentifi� avec un Bearer
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,  // on veut v�rifier que la cl� utilis�e pour signer le token est une cl� valide
        ValidateIssuer = true,  // on veut v�rifier que l'�metteur du token est valide
        ValidateAudience = true, // on veut v�rifier que le destinataire du token est valide
        ValidateLifetime = true, // on veut v�rifier que le token n'est pas expir�
        ClockSkew = TimeSpan.Zero, // on tol�re un d�calage de 0 minute
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"], // on v�rifie que l'�metteur du token est bien celui qu'on attend
        ValidAudience = builder.Configuration["JwtSettings:Audience"], // on v�rifie que le destinataire du token est bien celui qu'on attend
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)) // on v�rifie que la cl� utilis�e pour signer le token est bien celle qu'on attend
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
