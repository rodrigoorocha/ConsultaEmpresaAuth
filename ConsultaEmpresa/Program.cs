using Microsoft.EntityFrameworkCore;
using ConsultaEmpresa.Infra.Context;
using ConsultaEmpresa.Aplication.Services;
using ConsultaEmpresa.Domain.Features.Usuarios;
using ConsultaEmpresa.Infra.Repository.Usuarios;
using ConsultaEmpresa.Domain.Features.Shared;
using ConsultaEmpresa.Infra.Auth;
using Microsoft.OpenApi.Models;
using ConsultaEmpresa.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Register services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<GeradorDeTokenService>();

// Register the SenhaEncriptador service
builder.Services.AddScoped<SenhaEncriptador>();

// JWT configuration
var minutosDeDuracao = builder.Configuration.GetValue<uint>("Settings:JWT:minutosDeDuracao");
var chaveDeAssinatura = builder.Configuration.GetValue<string>("Settings:JWT:chaveDeAssinatura");

builder.Services.AddScoped<IGeradorDeTokenDeAcesso>(
            options => new GeradorDeTokenDeAcesso(minutosDeDuracao, chaveDeAssinatura));
builder.Services.AddScoped<GeradorDeTokenService>();
builder.Services.AddScoped<IValidadorToken>(opt => new ValidadorToken(chaveDeAssinatura));

// Add Authorization services
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Insira 'Bearer' [espa�o] e o token JWT. Exemplo: 'Bearer eyJhbGciOiJI...'"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();